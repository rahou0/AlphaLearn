using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
public class LUI_PAK : MonoBehaviour {

    [Header("VARIABLES")]
    public GameObject mainCanvas;
    public GameObject scriptObject;
    public Animator animatorComponent;
    public string animName;
     ManageSystemHand mngsyshands;
     ViewPanel viewPAnel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();
    void Start()
    {
        viewPAnel = GameObject.Find("ViewPanel").GetComponent<ViewPanel>();
        mngsyshands= GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();

        animatorComponent.GetComponent<Animator>();
        actions.Add("start", a => SpeechAction("start"));

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log(gameObject.transform.parent.gameObject.transform.parent.gameObject.name);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            viewPAnel.currentPanel = "Main Panel";
            animatorComponent.Play(animName);
            mainCanvas.SetActive(true);
            Destroy(scriptObject);
        }
        if(!mngsyshands.IsOpenL && !mngsyshands.IsOpenR && viewPAnel.currentPanel == gameObject.transform.parent.gameObject.transform.parent.gameObject.name)
        {
            viewPAnel.currentPanel = "Main Panel";
            viewPAnel.MakeAction();
            animatorComponent.Play(animName);
            mainCanvas.SetActive(true);
            Destroy(scriptObject);
        }
    }
    void GestureAction()
    {

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke("");
    }
    void SpeechAction(string word)
    {
        viewPAnel.currentPanel = "Main Panel";
        animatorComponent.Play(animName);
        mainCanvas.SetActive(true);
        Destroy(scriptObject);
    }
}