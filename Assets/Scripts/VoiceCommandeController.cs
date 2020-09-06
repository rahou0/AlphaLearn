using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class VoiceCommandeController : MonoBehaviour
{
    SearchIndex sindex;
    GameManagerSystem gameManagerSystem;
    GameObject cubeR, cubeL;
    Vector3 oldPositionCubeR, oldPositionCubeL;
    bool rightHandEmpty = true, leftHandEmpty = true;
    public GameObject aRightHand;
    public GameObject aLeftHand;
    Text message;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();
    // Start is called before the first frame update
    void Start()
    {
        sindex = GameObject.Find("SearchIndex").GetComponent<SearchIndex>();
        gameManagerSystem = GameObject.Find("GameManagerSystem").GetComponent<GameManagerSystem>();
        message = GameObject.Find("message").GetComponent<Text>();

        AddAlphabiticAction();
        actions.Add("Free my hands", a => FreeHand("both"));
        actions.Add("free left hand", a => FreeHand("left"));
        actions.Add("free right hand", a => FreeHand("right"));
        actions.Add("check my hands", a => Validate("both"));
        actions.Add("check left", a => Validate("left"));
        actions.Add("check right", a => Validate("right"));
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke("");
    }

    #region select alphabitic Box
    void SelectBox(string alphabet)
    {
        if (!gameManagerSystem.gameOver)
        {
            if (GameObject.Find("Cube " + alphabet) == null)
            {
                message.text = "the alphabet that are u looking for not exist in this level";
            }
            else
            {
                if (!rightHandEmpty && !leftHandEmpty)
                {
                    message.text = "The two hand already holding cubes";
                }
                else
                {
                    if (GameObject.Find("Cube " + alphabet).GetComponent<Rigidbody>().isKinematic == true)
                        message.text = "This box already selected";
                    else
                    {
                        message.text = "Box " + alphabet + " is selected";
                        if (rightHandEmpty)
                        {
                            cubeR = GameObject.Find("Cube " + alphabet);
                            oldPositionCubeR = cubeR.transform.position;
                            StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                            rightHandEmpty = false;
                        }
                        else
                        {
                            cubeL = GameObject.Find("Cube " + alphabet);
                            oldPositionCubeL = cubeL.transform.position;
                            StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                            leftHandEmpty = false;
                        }
                    }
                }
            }
            EnableText();
            Invoke("DisableText", 2f);
        }
        
    }
    #endregion
    
    #region free hands ("left or right or both")
    void FreeHand(string hand)
    {
        if (!gameManagerSystem.gameOver)
        {
            if (hand == "both")
            {
                if (leftHandEmpty && rightHandEmpty)
                    message.text = "the two hands already empty!";
                else
                {
                    if (!leftHandEmpty)
                        FreeLeftHand("");
                    if (!rightHandEmpty)
                        FreeRightHand("");
                    message.text = "your hands are free now!";
                }
            }
            else
            {
                if (hand == "left")
                {
                    if (leftHandEmpty)
                        message.text = "this hand already empty!";
                    else
                        FreeLeftHand("Box is Released");
                }
                else
                {
                    if (rightHandEmpty)
                        message.text = "this hand already empty!";
                    else
                        FreeRightHand("Box is Released");
                }
            }
            EnableText();
            Invoke("DisableText", 2f);
        }
        
    }
    void FreeRightHand(string msg)
    {
        StartCoroutine(ReturnObject(aRightHand, cubeR, aRightHand.transform.position, oldPositionCubeR, 0.5f));
        rightHandEmpty = true;
        cubeR = null;
        message.text = msg;
    }
    void FreeLeftHand(string msg)
    {
        StartCoroutine(ReturnObject(aLeftHand, cubeL, aLeftHand.transform.position, oldPositionCubeL, 0.5f));
        leftHandEmpty = true;
        cubeL = null;
        message.text = msg;
    }
    #endregion
    
    #region vzlidate hands ("left or right or both")
    void Validate(String hand)
    {
        if (!gameManagerSystem.gameOver)
        {
            if (hand == "both")
            {
                if (leftHandEmpty && rightHandEmpty)
                    ShowMsg("you must select one alphabet at least!");
                else
                {
                    if (!leftHandEmpty)
                        ValidateLeftHand();
                    if (!rightHandEmpty)
                        ValidateRightHand();
                }
            }
            else
            {
                if (hand == "left")
                {
                    if (leftHandEmpty)
                        ShowMsg("u must hold alphabet in your left hand to validate!");
                    else
                    {
                        ValidateLeftHand();
                    }

                }
                else
                {
                    if (rightHandEmpty)
                        ShowMsg("u must hold alphabet in your right hand to validate!");
                    else
                        ValidateRightHand();
                }
            }
        }
        
}
    void ValidateRightHand()
    {
        cubeR.GetComponent<Rigidbody>().isKinematic = false;
        float gravity = Physics.gravity.magnitude;
        Vector3 direction = cubeR.transform.position - GameObject.Find("PortalHole").transform.position;
        float initialVelocity = CalculateJumpSpeed(1f, gravity);
        cubeR.GetComponent<Rigidbody>().AddForce(-initialVelocity * direction, ForceMode.Impulse);
        RemoveGlow(cubeR);
        rightHandEmpty = true;
        cubeR = null;
    }
    void ValidateLeftHand()
    {
        cubeL.GetComponent<Rigidbody>().isKinematic = false;
        float gravity = Physics.gravity.magnitude;
        Vector3 direction = cubeL.transform.position - GameObject.Find("PortalHole").transform.position;
        float initialVelocity = CalculateJumpSpeed(1f, gravity);
        cubeL.GetComponent<Rigidbody>().AddForce(-initialVelocity * direction, ForceMode.Impulse);
        RemoveGlow(cubeL);
        leftHandEmpty = true;
        cubeL = null;
    }
    #endregion
    
    #region methods for Manipulating UI
    void DisableText()
    {
        message.enabled = false;
        GameObject.Find("Dialoge").GetComponent<Image>().enabled = false;
        GameObject.Find("IconMsg").GetComponent<Image>().enabled = false;
    }
    void EnableText()
    {
        GameObject.Find("Dialoge").GetComponent<Image>().enabled = true;
        GameObject.Find("IconMsg").GetComponent<Image>().enabled = true;
        message.enabled = true;
    }
    void ShowMsg(string msg)
    {
        message.text = msg;
        EnableText();
        Invoke("DisableText", 2f);
    }
    #endregion

    #region general methods
    private float CalculateJumpSpeed(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    #endregion

    #region movehands and boxes
    IEnumerator MoveObject(GameObject hand,GameObject cube,Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            hand.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.position = target;
        AddGlow(cube);
        float startTimeback = Time.time;
        while (Time.time < startTimeback + overTime)
        {
            hand.transform.position = Vector3.Lerp(target,source, (Time.time - startTimeback) / overTime);
            cube.transform.position = Vector3.Lerp(target, source, (Time.time - startTimeback) / overTime);
            cube.transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return null;
        }
        transform.position = target;
    }
    IEnumerator ReturnObject(GameObject hand, GameObject cube, Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            hand.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            cube.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            cube.transform.rotation = Quaternion.Euler(0, 0, 0); 
            yield return null;
        }
        transform.position = target;
        RemoveGlow(cube);
        float startTimeback = Time.time;
        while (Time.time < startTimeback + overTime)
        {
            hand.transform.position = Vector3.Lerp(target, source, (Time.time - startTimeback) / overTime);
            yield return null;
        }
        transform.position = target;
    }
    IEnumerator MoveBothsHands(GameObject hand1,GameObject hand2,GameObject cube1, GameObject cube2, Vector3 source1, Vector3 source2, Vector3 target1, Vector3 target2, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            hand1.transform.position = Vector3.Lerp(source1, target1, (Time.time - startTime) / overTime);
            cube1.transform.position = Vector3.Lerp(source1, target1, (Time.time - startTime) / overTime);
            cube1.transform.rotation = Quaternion.Euler(0, 0, 0);
            hand1.transform.position = Vector3.Lerp(source2, target2, (Time.time - startTime) / overTime);
            cube2.transform.position = Vector3.Lerp(source2, target2, (Time.time - startTime) / overTime);
            cube2.transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return null;
        }
        transform.position = target1;
        RemoveGlow(cube1);
        RemoveGlow(cube2);
        float startTimeback = Time.time;
        while (Time.time < startTimeback + overTime)
        {
            hand1.transform.position = Vector3.Lerp(target1, source1, (Time.time - startTimeback) / overTime);
            hand2.transform.position = Vector3.Lerp(target2, source2, (Time.time - startTimeback) / overTime);
            yield return null;
        }
        transform.position = target1;
    }
    IEnumerator WaitFor1Second()
    {
        yield return new WaitForSeconds(3);
    }
    #endregion
    
    #region add and remove glow effect to boxes
    void AddGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        gObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    void RemoveGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        gObject.GetComponent<Rigidbody>().isKinematic = false;

    }
    #endregion
    
    #region methods that add voice commands
    void AddAlphabiticAction()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "SpeechEasyLvl1":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                break;
            case "SpeechEasyLvl2":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box T", a => SelectBox("T"));
                break;
            case "SpeechEasyLvl3":
                actions.Add("Select box W", a => SelectBox("W"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box D", a => SelectBox("D"));
                break;
            case "SpeechEasyLvl4":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box D", a => SelectBox("D"));
                break;
            case "SpeechEasyLvl5":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box T", a => SelectBox("T"));
                break;
            case "SpeechEasyLvl6":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box G", a => SelectBox("G"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box J", a => SelectBox("J"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechMediumLvl1":
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box L", a => SelectBox("L"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box W", a => SelectBox("W"));
                actions.Add("Select box T", a => SelectBox("T"));
                break;
            case "SpeechMediumLvl2":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box B", a => SelectBox("B"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechMediumLvl3":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box G", a => SelectBox("G"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box J", a => SelectBox("J"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box T", a => SelectBox("T"));
                break;
            case "SpeechMediumLvl4":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box L", a => SelectBox("L"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box B", a => SelectBox("B"));
                break;
            case "SpeechMediumLvl5":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box G", a => SelectBox("G"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box J", a => SelectBox("J"));
                actions.Add("Select box P", a => SelectBox("P"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box H", a => SelectBox("H"));
                break;
            case "SpeechMediumLvl6":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box G", a => SelectBox("G"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechHardLvl1":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box T", a => SelectBox("T"));
                break;
            case "SpeechHardLvl2":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box D", a => SelectBox("D"));
                actions.Add("Select box V", a => SelectBox("V"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechHardLvl3":
                actions.Add("Select box D", a => SelectBox("D"));
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechHardLvl4":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box G", a => SelectBox("G"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                break;
            case "SpeechHardLvl5":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box H", a => SelectBox("H"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box L", a => SelectBox("L"));
                actions.Add("Select box R", a => SelectBox("R"));
                break;
            case "SpeechHardLvl6":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                break;
            case "SpeechVeryHardLvl1":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box H", a => SelectBox("H"));
                actions.Add("Select box L", a => SelectBox("L"));
                break;
            case "SpeechVeryHardLvl2":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box L", a => SelectBox("L"));
                actions.Add("Select box I", a => SelectBox("I"));
                break;
            case "SpeechVeryHardLvl3":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box Y", a => SelectBox("Y"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box U", a => SelectBox("U"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box O", a => SelectBox("O"));
                break;
            case "SpeechVeryHardLvl4":
                actions.Add("Select box O", a => SelectBox("O"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box P", a => SelectBox("P"));
                actions.Add("Select box D", a => SelectBox("D"));
                break;
            case "SpeechVeryHardLvl5":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box L", a => SelectBox("L"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box Y", a => SelectBox("Y"));
                actions.Add("Select box S", a => SelectBox("S"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box P", a => SelectBox("P"));
                actions.Add("Select box D", a => SelectBox("D"));
                break;
            case "SpeechVeryHardLvl6":
                actions.Add("Select box A", a => SelectBox("A"));
                actions.Add("Select box T", a => SelectBox("T"));
                actions.Add("Select box E", a => SelectBox("E"));
                actions.Add("Select box K", a => SelectBox("K"));
                actions.Add("Select box F", a => SelectBox("F"));
                actions.Add("Select box M", a => SelectBox("M"));
                actions.Add("Select box Q", a => SelectBox("Q"));
                actions.Add("Select box R", a => SelectBox("R"));
                actions.Add("Select box C", a => SelectBox("C"));
                actions.Add("Select box N", a => SelectBox("N"));
                actions.Add("Select box I", a => SelectBox("I"));
                actions.Add("Select box H", a => SelectBox("H"));
                break;
            default:
                break;
        }
    }
    void GameOverLIst()
    {

    }
    void DoMenuCommande(String commande)
    {
        switch (commande)
        {
            case "again":
                break;
            case "exit":
                Debug.Log("Exit");
                break;
            case "restart":
                break;
            case "setting":
                break;
            case "help":
                break;
            default:
                break;
        }
    }
    #endregion
}
