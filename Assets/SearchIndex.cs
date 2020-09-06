using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class SearchIndex : MonoBehaviour
{
    public Dictionary<string, LEVEL> actionsUI = new Dictionary<string, LEVEL>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddLevelAction()
    {
        if (actionsUI.Count != 48)
        {
            actionsUI.Add("GestureEasyLvl1", new LEVEL(1, "CAT"));
            actionsUI.Add("GestureEasyLvl2", new LEVEL(2, "MAN"));
            actionsUI.Add("GestureEasyLvl3", new LEVEL(3, "WIND"));
            actionsUI.Add("GestureEasyLvl4", new LEVEL(4, "CARD"));
            actionsUI.Add("GestureEasyLvl5", new LEVEL(5, "FACE"));
            actionsUI.Add("GestureEasyLvl6", new LEVEL(6, "FLAG"));
            actionsUI.Add("GestureMediumLvl1", new LEVEL(1, "TWIN"));
            actionsUI.Add("GestureMediumLvl2", new LEVEL(2, "ALBUM"));
            actionsUI.Add("GestureMediumLvl3", new LEVEL(3, "AGENT"));
            actionsUI.Add("GestureMediumLvl4", new LEVEL(4, "BRAIN"));
            actionsUI.Add("GestureMediumLvl5", new LEVEL(5, "CHEAP"));
            actionsUI.Add("GestureMediumLvl6", new LEVEL(6, "EMAIL"));
            actionsUI.Add("GestureHardLvl1", new LEVEL(1, "FRUIT"));
            actionsUI.Add("GestureHardLvl2", new LEVEL(2, "ADVICE"));
            actionsUI.Add("GestureHardLvl3", new LEVEL(3, "DREAMS"));
            actionsUI.Add("GestureHardLvl4", new LEVEL(4, "FINGER"));
            actionsUI.Add("GestureHardLvl5", new LEVEL(5, "LAUNCH"));
            actionsUI.Add("GestureHardLvl6", new LEVEL(6, "MARKET"));
            actionsUI.Add("GestureVeryHardLvl1", new LEVEL(1, "MOTHER"));
            actionsUI.Add("GestureVeryHardLvl2", new LEVEL(2, "ARTICLE"));
            actionsUI.Add("GestureVeryHardLvl3", new LEVEL(3, "COUNTRY"));
            actionsUI.Add("GestureVeryHardLvl4", new LEVEL(4, "DESKTOP"));
            actionsUI.Add("GestureVeryHardLvl5", new LEVEL(5, "DISPLAY"));
            actionsUI.Add("GestureVeryHardLvl6", new LEVEL(6, "MACHINE"));
            actionsUI.Add("SpeechEasyLvl1", new LEVEL(1, "CAT"));
            actionsUI.Add("SpeechEasyLvl2", new LEVEL(2, "MAN"));
            actionsUI.Add("SpeechEasyLvl3", new LEVEL(3, "WIND"));
            actionsUI.Add("SpeechEasyLvl4", new LEVEL(4, "CARD"));
            actionsUI.Add("SpeechEasyLvl5", new LEVEL(5, "FACE"));
            actionsUI.Add("SpeechEasyLvl6", new LEVEL(6, "FLAG"));
            actionsUI.Add("SpeechMediumLvl1", new LEVEL(1, "TWIN"));
            actionsUI.Add("SpeechMediumLvl2", new LEVEL(2, "ALBUM"));
            actionsUI.Add("SpeechMediumLvl3", new LEVEL(3, "AGENT"));
            actionsUI.Add("SpeechMediumLvl4", new LEVEL(4, "BRAIN"));
            actionsUI.Add("SpeechMediumLvl5", new LEVEL(5, "CHEAP"));
            actionsUI.Add("SpeechMediumLvl6", new LEVEL(6, "EMAIL"));
            actionsUI.Add("SpeechHardLvl1", new LEVEL(1, "FRUIT"));
            actionsUI.Add("SpeechHardLvl2", new LEVEL(2, "ADVICE"));
            actionsUI.Add("SpeechHardLvl3", new LEVEL(3, "DREAMS"));
            actionsUI.Add("SpeechHardLvl4", new LEVEL(4, "FINGER"));
            actionsUI.Add("SpeechHardLvl5", new LEVEL(5, "LAUNCH"));
            actionsUI.Add("SpeechHardLvl6", new LEVEL(6, "MARKET"));
            actionsUI.Add("SpeechVeryHardLvl1", new LEVEL(1, "MOTHER"));
            actionsUI.Add("SpeechVeryHardLvl2", new LEVEL(2, "ARTICLE"));
            actionsUI.Add("SpeechVeryHardLvl3", new LEVEL(3, "COUNTRY"));
            actionsUI.Add("SpeechVeryHardLvl4", new LEVEL(4, "DESKTOP"));
            actionsUI.Add("SpeechVeryHardLvl5", new LEVEL(5, "DISPLAY"));
            actionsUI.Add("SpeechVeryHardLvl6", new LEVEL(6, "MACHINE"));
        }
    }
}
