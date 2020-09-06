using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Windows.Speech;
public class VoiceCommandeController : MonoBehaviour
{
    GameObject cubeR, cubeL;
    Vector3 oldPositionCubeR, oldPositionCubeL;
    bool rightHandEmpty = true, leftHandEmpty = true;
    public GameObject aRightHand;
    public GameObject aLeftHand;
    public Text message;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Validate my hands", Validate);
        actions.Add("Move", Go);
        actions.Add("Select box R", SelectBoxR);
        actions.Add("Select box M", SelectBoxM);
        actions.Add("Select box B", SelectBoxB);
        actions.Add("Select box T", SelectBoxT);
        actions.Add("Select box O", SelectBoxO);
        actions.Add("Select box Y", SelectBoxY);
        actions.Add("free left hand", ReleaseLeftHand);
        actions.Add("free right hand", ReleaseRightHand);
        actions.Add("please again", PleaseRepeat);
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
        actions[speech.text].Invoke();
    }
    #region select alphabitic Box
    void SelectBoxR()
    {
        if(!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding cubes";
        }
        else
        {            
            message.text = "Box R is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube R");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
            }
            else
            {
                cubeL = GameObject.Find("Cube R");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void SelectBoxM()
    {
        if (!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding cubes";
        }
        else
        {
            message.text = "Box M is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube M");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
            }
            else
            {
                cubeL = GameObject.Find("Cube M");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void SelectBoxB()
    {
        if (!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding boxes";
        }
        else
        {
            message.text = "Box B is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube B");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
            }
            else
            {
                cubeL = GameObject.Find("Cube B");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void SelectBoxT()
    {
        if (!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding cubes";
        }
        else
        {
            message.text = "Box T is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube T");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
                cubeR = GameObject.Find("Cube T");
            }
            else
            {
                cubeL = GameObject.Find("Cube T");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void SelectBoxO()
    {
        if (!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding cubes";
        }
        else
        {
            message.text = "Box O is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube O");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
            }
            else
            {
                cubeL = GameObject.Find("Cube O");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void SelectBoxY()
    {
        if (!rightHandEmpty && !leftHandEmpty)
        {
            message.text = "The two hand already holding cubes";
        }
        else
        {
            message.text = "Box Y is selected";
            if (rightHandEmpty)
            {
                cubeR = GameObject.Find("Cube Y");
                oldPositionCubeR = cubeR.transform.position;
                StartCoroutine(MoveObject(aRightHand, cubeR, aRightHand.transform.position, cubeR.transform.position, 0.5f));
                rightHandEmpty = false;
            }
            else
            {
                cubeL = GameObject.Find("Cube Y");
                oldPositionCubeL = cubeL.transform.position;
                StartCoroutine(MoveObject(aLeftHand, cubeL, aLeftHand.transform.position, cubeL.transform.position, 0.5f));
                leftHandEmpty = false;
            }
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    #endregion
    #region free hands left or right or both
    void ReleaseLeftHand()
    {
        if (leftHandEmpty)
        {
            message.text = "this hand already empty!";
        }else
        {
            StartCoroutine(ReturnObject(aLeftHand, cubeL, aLeftHand.transform.position, oldPositionCubeL, 0.5f));
            leftHandEmpty = true;
            cubeL = null;
            message.text = "Box is Released";
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void ReleaseRightHand()
    {
        if (rightHandEmpty)
        {
            message.text = "this hand already empty!";
        }
        else
        {
            StartCoroutine(ReturnObject(aRightHand, cubeR, aRightHand.transform.position, oldPositionCubeR, 0.5f));
            rightHandEmpty = true;
            cubeR = null;
            message.text = "Cube is Released";
        }
        EnableText();
        Invoke("DisableText", 2f);
    }
    void Validate()
    {
        if (!leftHandEmpty && !rightHandEmpty)
        {
            StartCoroutine(MoveBothsHands(aRightHand,aLeftHand,cubeR,cubeL ,aRightHand.transform.position, aLeftHand.transform.position, new Vector3(-0.19f, 2, 24.67f), new Vector3(1, 2, 24.67f),0.5f));
        }
        else
        {
            message.text = "";
        }
    }
    #endregion
    void PleaseRepeat()
    {
        message.text = "Okey i will repeat for u <3 listen!";
        EnableText();
        Invoke("DisableText", 2f);
    }
    void Go()
    {
        //
    }
    void DisableText()
    {
        message.enabled = false;
        GameObject.Find("Dialoge").GetComponent<Image>().enabled = false;
        GameObject.Find("IconMsg").GetComponent<Image>().enabled = false;
    }
    void EnableText()
    {
        message.enabled = true;
        GameObject.Find("Dialoge").GetComponent<Image>().enabled = true;
        GameObject.Find("IconMsg").GetComponent<Image>().enabled = true;
    }
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
   
}
