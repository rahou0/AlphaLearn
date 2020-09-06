using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerSystem : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();

    public bool gameOver = false, win = false, gamePause = false, gameExit = false ,isenabled=true;
    public GameObject WinUI, GameOverUI, GamePauseUI, GameExitUI;
    // Start is called before the first frame update
    public void EndGame()
    {
        GameOverUI.SetActive(true);
        gameOver = true;
    }
    public void ExitGame(string namebutton)
    {
        StartCoroutine(ExecuteAfterTimePanel(0.5f, namebutton));
    }
    public void PauseGame()
    {
        StartCoroutine(ExecuteAfterTimePanel(0.5f, "PauseButton"));
    }
    public void ResumeGame()
    {
        if (gameExit)
        {
            StartCoroutine(ExecuteAfterTimePanel(0.5f, "BackButton"));
        }
        else
        {
            GamePauseUI.SetActive(false);
            gamePause = false;
        }
    }
    public void RestartGame(string namebutton)
    {
        StartCoroutine(ExecuteAfterTimePanel(0.5f, namebutton));
   }
    public void WinLevel()
    {
        win = true;
        WinUI.SetActive(true);
    }
    public void Pronounce()
    {
        StartCoroutine(ExecuteAfterTime(1f, "SpeechButton"));
    }
    public void EnableHeadT()
    {
        if(!isenabled)
            StartCoroutine(ExecuteAfterTimePanel(1f, "HeadTracking"));
    }
    public void DesibaleHT()
    {
        if (isenabled)
            StartCoroutine(ExecuteAfterTimePanel(1f, "HeadTracking"));
    }
    void Start()
    {

        actions.Add("exit", a => StartVAction("exit"));
        actions.Add("stop", a => StartVAction("stop"));
        actions.Add("word", a => StartVAction("word"));
        actions.Add("follow", a => StartVAction("follow"));
        actions.Add("restart", a => StartVAction("restart"));
        actions.Add("back", a => StartVAction("back"));
        actions.Add("pause", a => StartVAction("pause"));
        actions.Add("help", a => StartVAction("help"));
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke("");
    }
    void StartVAction(string word)
    {
        switch (word)
        {
            case "exit":
                if (!gamePause && !gameExit && !gameOver)
                {
                    ExitGame("ExitButton");
                }
                else
                {
                    if(gameExit && !gamePause)
                    {
                        ExitGame("ExitButtonFULL");
                    }else
                    {
                        if(!gamePause && !gameExit && gameOver)
                        {
                            ExitGame("ExitButtonGO");
                        }
                    }
                }
                break;
            case "pause":
                if (!gamePause && !gameExit && !gameOver)
                {
                    PauseGame();
                }
                break;
            case "word":
                if(!gamePause && !gameExit&&!gameOver)
                {
                    Pronounce();
                }
                break;
            case "help":
                if (!gamePause && !gameExit &&!gameOver)
                {
                    Pronounce();
                }
                break;
            case "back":
                    ResumeGame();
                break;
            case "restart":
                if (gameOver)
                {
                    RestartGame("RestartButtonGO");
                }
                else
                {
                    if(!gamePause && !gameExit)
                        RestartGame("RestartButton");
                }
                break;
            case "follow":
                if (!gameOver && !gamePause && !gameExit)
                {
                    if (SceneManager.GetActiveScene().name.StartsWith("Gesture")){EnableHeadT();}
                }
                break;
            case "stop":
                if (!gameOver && !gamePause && !gameExit)
                {
                    if (SceneManager.GetActiveScene().name.StartsWith("Gesture")){DesibaleHT();}
                }
                break;
        }
    }

        // Update is called once per frame
        void Update()
    {

    }
    IEnumerator ExecuteAfterTime(float time, string objectName)
    {
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Pressed", true);
        FindObjectOfType<PronounceTheWord>().pronounceWord = true;
        yield return new WaitForSeconds(time);
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Pressed", false);
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Normal", true);
    }
    IEnumerator ExecuteAfterTimePanel(float time, string objectName)
    {
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Pressed", true);
        yield return new WaitForSeconds(time);
        GameObject.Find(objectName).GetComponent<Animator>().SetBool("Normal", true);
        switch (objectName)
        {
            case "ExitButton":
                    GameExitUI.SetActive(true);
                    gameExit = true;
                break;
            case "PauseButton":
                GamePauseUI.SetActive(true);
                gamePause = true;
                break;
            case "ExitButtonGO":
                GameExitUI.SetActive(true);
                gameExit = true;
                GameOverUI.SetActive(false);
                break;
            case "ExitButtonFULL":
                if (GameObject.Find("Head") != null)
                {
                    isenabled = false;
                    GameObject.Find("Head").GetComponent<HeadTracking>().enabled = false;
                }
                SceneManager.LoadScene("Home");
                break;
            case "RestartButton":
                if (GameObject.Find("Head") != null)
                {
                    isenabled = false;
                    GameObject.Find("Head").GetComponent<HeadTracking>().enabled = false;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "RestartButtonGO":
                if (GameObject.Find("Head") != null)
                {
                    isenabled = false;
                    GameObject.Find("Head").GetComponent<HeadTracking>().enabled = false;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "BackButton":
                if (gameOver)
                {
                    GameExitUI.SetActive(false);
                    gameExit = false;
                    GameOverUI.SetActive(true);
                }
                else
                {
                    GameExitUI.SetActive(false);
                    gameExit = false;
                }
                break;
            case "HeadTracking":
                if (isenabled)
                {
                    if (GameObject.Find("Head") != null)
                    {
                        isenabled = false;
                        GameObject.Find("Main Camera (1)").transform.position = new Vector3(0, 10, 40);
                        GameObject.Find("Main Camera (1)").transform.rotation=Quaternion.Euler(20, 180,0); ;
                    }
                    GameObject.Find("IconEnable").GetComponent<Image>().color = Color.white;
                    isenabled = false;
                }
                else
                {
                    if (GameObject.Find("Head") != null)
                    {
                        GameObject.Find("Head").GetComponent<HeadTracking>().enabled = true;
                        GameObject.Find("IconEnable").GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                        isenabled = true;
                    }
                }
                    
             break;
        }
    }
}
