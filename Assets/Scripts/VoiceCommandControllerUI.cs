using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VoiceCommandControllerUI : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    ViewPanel viewPAnel;
    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        viewPAnel = GameObject.Find("ViewPanel").GetComponent<ViewPanel>();
        actions.Add("hand mode", a => Selection("hand"));
        actions.Add("gestures mode", a => Selection("gesture"));
        actions.Add("Speech", a => Selection("speech"));
        actions.Add("Help", a => Selection("help"));
        actions.Add("Alphabet", a => Selection("alphabet"));
        actions.Add("back", a => Selection("back"));
        actions.Add("setting", a => Selection("setting"));
        actions.Add("about", a => Selection("about"));
        actions.Add("exit", a => Selection("exit"));
        actions.Add("yes", a => Selection("yes"));
        actions.Add("no", a => Selection("no"));
        actions.Add("general mode", a => Selection("full"));
        actions.Add("examples", a => Selection("examples"));
        actions.Add("Easy", a => Selection("Easy"));

        actions.Add("Medium", a => Selection("Medium"));
        actions.Add("start video", a => Selection("start video"));

        actions.Add("head movement", a => Selection("head movement"));
        actions.Add("head tracking", a => Selection("head tracking"));
        actions.Add("example one", a => Selection("example one"));
        actions.Add("example two", a => Selection("example two"));
        actions.Add("example three", a => Selection("example three"));
        actions.Add("example four", a => Selection("example four"));





        actions.Add("Hard", a => Selection("Hard"));
        actions.Add("Very hard", a => Selection("Very hard"));

        //actions.Add("Move", Go);
        //actions.Add("please again", PleaseRepeat);
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
    void Selection(string word)
    {
        switch (word)
        {
            case "gesture":
                if (viewPAnel.currentPanel.Equals("Armory Panel"))
                StartCoroutine(ExecuteAfterTimeGAME(.2f, "M4A1", "Armory Panel"));
                break;
            case "full":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(.2f, "Campaign", "Campaign Panel"));
                break;
            case "hand":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(.2f, "Campaign", "Campaign Panel"));
                Debug.Log("hand Mode");
                break;
            case "speech":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Online", "Online Panel"));
                else
                    if(viewPAnel.currentPanel.Equals("Armory Panel"))
                        StartCoroutine(ExecuteAfterTimeGAME(0.2f, "AKM", "Armory Panel"));
                break;
            case "help":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Armory", "Armory Panel"));
                break;
            case "alphabet":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Missions", "Missions Panel"));
                break;
            case "examples":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Missions", "Missions Panel"));
                break;
            case "setting":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Settings", "Settings Panel"));
                break;
            case "back":
                if (!viewPAnel.currentPanel.Equals("Main Panel") && !viewPAnel.currentPanel.Equals("Exit Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Back Reversed","Main Panel"));
                break;
            case "about":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "About", "About Panel"));
                break;
            case "exit":
                if (viewPAnel.currentPanel.Equals("Main Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Exit", "Exit Panel"));
                break;
            case "no":
                if (viewPAnel.currentPanel.Equals("Exit Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "No", "Main Panel"));
                break;
            case "yes":
                if (viewPAnel.currentPanel.Equals("Exit Panel"))
                    StartCoroutine(ExecuteAfterTime(0.2f, "Yes", "Main Panel"));
                break;
            case "Easy":
                if (viewPAnel.currentPanel.Equals("Online Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "EasySpeech", "Main Panel"));
                else
                    if(viewPAnel.currentPanel.Equals("Campaign Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Easy", "Main Panel"));
                break;
            case "Medium":
                if (viewPAnel.currentPanel.Equals("Online Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "MediumSpeech", "Main Panel"));
                else
                    if (viewPAnel.currentPanel.Equals("Campaign Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Medium", "Main Panel"));
                break;
            case "Hard":
                if (viewPAnel.currentPanel.Equals("Online Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "HardSpeech", "Main Panel"));
                else
                    if (viewPAnel.currentPanel.Equals("Campaign Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Hard", "Main Panel"));
                break;
            case "Very hard":
                if (viewPAnel.currentPanel.Equals("Online Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "VeryHardSpeech", "Main Panel"));
                else
                    if (viewPAnel.currentPanel.Equals("Campaign Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "VeryHard", "Main Panel"));
                break;
            case "head movement":
                if (viewPAnel.currentPanel.Equals("Armory Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "MP5 SD", "Armory Panel"));
                break;
            case "example one":
                if (viewPAnel.currentPanel.Equals("Missions Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Example One", "Missions Panel"));
                break;
            case "example two":
                if (viewPAnel.currentPanel.Equals("Missions Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Example Two", "Missions Panel"));
                break;
            case "example three":
                if (viewPAnel.currentPanel.Equals("Missions Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Example Three", "Missions Panel"));
                break;
            case "example four":
                if (viewPAnel.currentPanel.Equals("Missions Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Example Four", "Missions Panel"));
                break;
            case "head tracking":
                if (viewPAnel.currentPanel.Equals("Armory Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "MP5 SD", "Armory Panel"));
                break;
            case "start video":
                if (viewPAnel.currentPanel.Equals("Missions Panel"))
                    StartCoroutine(ExecuteAfterTimeGAME(0.2f, "Start", "Missions Panel"));
                break;
        }

    }
    IEnumerator ExecuteAfterTime(float time, string objectName,string nextdistination)
    {
        GameObject.Find(viewPAnel.currentPanel + objectName).GetComponent<Animator>().SetBool("Pressed", true);
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameObject.Find(viewPAnel.currentPanel + objectName).GetComponent<Animator>().SetBool("Normal", true);
        GameObject.Find(viewPAnel.currentPanel + objectName).GetComponent<Button>().onClick.Invoke();
        viewPAnel.currentPanel = nextdistination;
    }
    IEnumerator ExecuteAfterTimeGAME(float time, string objectName, string nextdistination)
    {
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Pressed", true);
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Normal", true);
        GameObject.Find(objectName).GetComponent<Button>().onClick.Invoke();
    }

}
