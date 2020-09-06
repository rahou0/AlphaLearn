using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadABCgame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGestureGameEasyLevel()
    {
        SceneManager.LoadScene("GestureEasyLvl1");
    }
    public void StartGestureGameMediumLevel()
    {
        SceneManager.LoadScene("GestureMediumLvl1");
    }
    public void StartGestureGameHardLevel()
    {
        SceneManager.LoadScene("GestureHardLvl1");
    }
    public void StartGestureGameVeryHardLevel()
    {
        SceneManager.LoadScene("GestureVeryHardLvl1");
    }
    public void StartSpeechGameEasyLevel()
    {
        SceneManager.LoadScene("SpeechEasyLvl1");
    }
    public void StartSpeechGameMeduimLevel()
    {
        SceneManager.LoadScene("SpeechMediumLvl1");
    }
    public void StartSpeechGameHardLevel()
    {
        SceneManager.LoadScene("SpeechHardLvl1");
    }
    public void StartSpeechGameVeryHardLevel()
    {
        SceneManager.LoadScene("SpeechVeryHardLvl1");
    }
    public void HeadMovementHelp()
    {
        if (GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled)
        {
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/lateralview1");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The point of view within the scene can change by following the user head movements.";
        }
        else
        {
            GameObject.Find("borderImageHelp").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/lateralview1");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The point of view within the scene can change by following the user head movements.";
        }
    }
    public void ModesHelp()
    {
         if (GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled)
        {
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "There are two modes: first one support hand gestures, head movement and speech, the second mode support only one modalitiy: speech";
        }
        else
        {
            GameObject.Find("borderImageHelp").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "There are two modes: first one support hand gestures, head movement and speech, the second mode support only one modalitiy: speech";

        }
    }
    public void GestureHelp()
    {
        if (GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled)
        {
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Control boxses via gestures");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The virtual hands avatar in our application are designed to follow learner hands in order to navigate in the virtual space or perform the corresponding actions (grab or release virtual objects).";
        }
        else
        {
            GameObject.Find("borderImageHelp").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled = true;
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The virtual hands avatar in our application are designed to follow learner hands in order to navigate in the virtual space or perform the corresponding actions (grab or release virtual objects).";
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Control boxses via gestures");
        }
    }
    public void SpeechHelp()
    {
        if (GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled)
        {
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The application analyzes the learner speech, and lets the virtual hands perform the corresponding actions. There are 27 commands implemented.";
        }
        else
        {
            GameObject.Find("borderImageHelp").GetComponent<Image>().enabled = true;
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().enabled = true;
            GameObject.Find("TextofHelpViewImage").GetComponent<Text>().text = "The application analyzes the learner speech, and lets the virtual hands perform the corresponding actions. There are 27 commands implemented."; 
            GameObject.Find("ImageViewHelpUser").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
        }
    }
    public void ExampleOne()
    {
        if (GameObject.Find("TutorielOut").GetComponent<Image>().enabled)
        {
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "";
        }
        else
        {
            GameObject.Find("TutorielOut").GetComponent<Image>().enabled = true;
            GameObject.Find("VideoSOON").GetComponent<Image>().enabled = true;
            GameObject.Find("TutorielIn").GetComponent<Image>().enabled = true;
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "General mode: medium Level 4";
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Control boxses via gestures");
        }
    }
    public void ExampleTwo()
    {
        if (GameObject.Find("TutorielOut").GetComponent<Image>().enabled)
        {
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/lateralview1");
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "General mode: Very hard Level 6";
        }
        else
        {
            GameObject.Find("TutorielOut").GetComponent<Image>().enabled = true;
            GameObject.Find("VideoSOON").GetComponent<Image>().enabled = true;
            GameObject.Find("TutorielIn").GetComponent<Image>().enabled = true;
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "Speech mode:  easy Level 3";
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
        }
    }
    public void ExampleThree()
    {
        if (GameObject.Find("TutorielOut").GetComponent<Image>().enabled)
        {
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "Speech mode:  Hard Level 1"; ;
        }
        else
        {
            GameObject.Find("TutorielOut").GetComponent<Image>().enabled = true;
            GameObject.Find("VideoSOON").GetComponent<Image>().enabled = true;
            GameObject.Find("TutorielIn").GetComponent<Image>().enabled = true;
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "";
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
        }
    }
    public void ExampleFour()
    {
        if (GameObject.Find("TutorielOut").GetComponent<Image>().enabled)
        {
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "";
        }
        else
        {

            GameObject.Find("TutorielOut").GetComponent<Image>().enabled = true;
            GameObject.Find("VideoSOON").GetComponent<Image>().enabled = true;
            GameObject.Find("TutorielIn").GetComponent<Image>().enabled = true;
            GameObject.Find("Selected Mission Title").GetComponent<Text>().text = "";
            GameObject.Find("TutorielIn").GetComponent<Image>().sprite = Resources.Load<Sprite>("HelpAssetsFolder/Speech modes");
        }
    }
    public void StartTutoriel()
    {
        if(GameObject.Find("Selected Mission Title").GetComponent<Text>().text.ToString()!="")
            Application.OpenURL("https://www.youtube.com/channel/UCRsF0J6f-7TEJsMbzqPa0-g");
    }
}
