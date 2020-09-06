using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class AbosrobKinect : MonoBehaviour
{
    SearchIndex sindex;
    public GameObject goParticalEffect;
    int counter = 1;
    GameManagerSystem gamemanager;
    string word;
    GameObject alphabetBox;
    List<GameObject> listofalphabet;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        sindex = GameObject.Find("SearchIndex").GetComponent<SearchIndex>();
        gamemanager = GameObject.Find("GameManagerSystem").GetComponent<GameManagerSystem>();
        sindex.AddLevelAction();
        GameObject.Find("NumLevel").GetComponent<Text>().text = "0" + sindex.actionsUI[SceneManager.GetActiveScene().name].indice;
        word = sindex.actionsUI[SceneManager.GetActiveScene().name].word;
        listofalphabet = new List<GameObject>();
        foreach (GameObject alphabet in GameObject.FindGameObjectsWithTag("alpha"))
        {
            listofalphabet.Add(alphabet);
        }
        ReorderAlpha();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            switch (other.gameObject.GetComponent<CubesController>().hand)
            {
                case 1:
                    GameObject.Find("HandLeft").GetComponent<ObjectDetectionLeftHand>().isSelected=false;
                    break;
                case 2:
                    GameObject.Find("HandRight").GetComponent<ObjectDetectionRightHand>().isSelected = false;
                    break;
            }
            char[] letter = new char[word.Length];
            letter = word.ToCharArray();
            if (other.gameObject.name == "Cube " + letter[i])
            {
                FindObjectOfType<AudioManagerSystem>().PlaySound("correct");
                alphabetBox = listofalphabet[i];
                alphabetBox.GetComponent<Text>().text = letter[i].ToString();
                StartCoroutine(AfterTime(1f, alphabetBox.transform.parent.gameObject.transform.parent.gameObject));
                goParticalEffect.GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
                i++;
                if (i == letter.Length)
                {
                    gamemanager.WinLevel();
                }
            }
            else
            {
                FindObjectOfType<AudioManagerSystem>().PlaySound("wrong");
                Addforce(other.gameObject);
            }
        }
    }
    void Addforce(GameObject cube)
    {
        float gravity = Physics.gravity.magnitude;
        Vector3 direction;
        if (counter%2==0)
             direction = cube.transform.position - GameObject.Find("Position1").transform.position;
        else
             direction = cube.transform.position - GameObject.Find("Position2").transform.position;
        float initialVelocity = CalculateJumpSpeed(1f, gravity);
        cube.GetComponent<Rigidbody>().AddForce(-initialVelocity * direction, ForceMode.Impulse);
    }
    #region general methods
    private float CalculateJumpSpeed(float jumpHeight, float gravity)
    {
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    #endregion
    IEnumerator AfterTime(float time, GameObject objectName)
    {
        objectName.GetComponent<Animator>().SetBool("Pressed", true);
        yield return new WaitForSeconds(time);
        objectName.GetComponent<Animator>().SetBool("Pressed", false);
        objectName.GetComponent<Animator>().SetBool("Normal", true);
    }
    void ReorderAlpha()
    {
        for (int i = 0; i < listofalphabet.Count - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < listofalphabet.Count; j++)
            {
                if (listofalphabet[min].transform.parent.gameObject.transform.parent.gameObject.name != listofalphabet[j].transform.parent.gameObject.transform.parent.gameObject.name)
                {
                    int s1 = Int16.Parse(listofalphabet[min].transform.parent.gameObject.transform.parent.gameObject.name.ToString().Remove(0, 10).Remove(1));
                    int s2 = Int16.Parse(listofalphabet[j].transform.parent.gameObject.transform.parent.gameObject.name.ToString().Remove(0, 10).Remove(1));
                    Debug.Log("S1 = " + s1 + " | S2 =" + s2);
                    if (s2 < s1)
                    {
                        min = j;

                    }
                }

            }
            if (min != i)
            {
                GameObject tempogm = listofalphabet[i];
                listofalphabet[i] = listofalphabet[min];
                listofalphabet[min] = tempogm;
            }
        }
    }
}
