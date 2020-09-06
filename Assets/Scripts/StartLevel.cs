using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public Text text;
    public SearchIndex sindex;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        sindex.AddLevelAction();
        text.text = "LEVEL 0" + sindex.actionsUI[SceneManager.GetActiveScene().name].indice;
    }

    public void DestroyObject()
    {
        FindObjectOfType<PronounceTheWord>().pronounceWord = true;
        DestroyObject(GameObject.Find("StartLevel"));

    }
}
