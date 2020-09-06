using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class AbsorbBoxes : MonoBehaviour
{
    SearchIndex sindex;
    public GameObject goParticalEffect;
    public Text levelID;
    GameManagerSystem gamemanager;
    string word;
    GameObject alphabetBox;
    List<GameObject> listofalphabet;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        sindex = GameObject.Find("SearchIndex").GetComponent<SearchIndex>();
        gamemanager = GameObject.Find("GameManagerSystem").GetComponent<GameManagerSystem>();
        Cursor.visible = false;
        sindex.AddLevelAction();
        GameObject.Find("NumLevel").GetComponent<Text>().text = "0" + sindex.actionsUI[SceneManager.GetActiveScene().name].indice;
        word = sindex.actionsUI[SceneManager.GetActiveScene().name].word;
        //word = sindex.actionsUI[SceneManager.GetActiveScene().name].word;
        listofalphabet = new List<GameObject>();
        foreach (GameObject alphabet in GameObject.FindGameObjectsWithTag("alpha"))
            {
                listofalphabet.Add(alphabet);
        }
        ReorderAlpha();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            char[] letter = new char[word.Length];
            letter = word.ToCharArray();
            if (i < letter.Length)
            {
                if (other.gameObject.name == "Cube " + letter[i])
                {
                    FindObjectOfType<AudioManagerSystem>().PlaySound("correct");
                    alphabetBox = listofalphabet[i];
                    alphabetBox.GetComponent<Text>().text = letter[i].ToString();
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
                    //Addforce(other.gameObject);
                }
            }
           
        }
    }
    void ReorderAlpha()
    {
        for (int i=0;i< listofalphabet.Count-1; i++)
        {
            int min = i;
            for (int j = i+1; j < listofalphabet.Count; j++)
            {
                if(listofalphabet[min].transform.parent.gameObject.transform.parent.gameObject.name != listofalphabet[j].transform.parent.gameObject.transform.parent.gameObject.name)
                {
                    int s1 = Int16.Parse(listofalphabet[min].transform.parent.gameObject.transform.parent.gameObject.name.ToString().Remove(0, 10).Remove(1));
                    int s2 = Int16.Parse(listofalphabet[j].transform.parent.gameObject.transform.parent.gameObject.name.ToString().Remove(0, 10).Remove(1));
                    Debug.Log("S1 = "+s1+" | S2 ="+s2);
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
