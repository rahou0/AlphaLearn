using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    float diviseur;
    public float timertime = 20f;
    public Text timerText;
    public Image hexaImgTimer,LeftBarTimer,RightBarTimer;
    //GameManagerSystem gamemnger;
    public GameManagerSystem gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        diviseur = timertime;
        //Debug.Log(gamemnger.gameOver);
    }

    // Update is called once per frame
    void Update()
    {
        if (timertime >0f && !gamemanager.gameOver && !gamemanager.gamePause && !gamemanager.win && !gamemanager.gameExit)
        {
            timertime -= Time.deltaTime;
            hexaImgTimer.fillAmount = timertime/diviseur;
            LeftBarTimer.fillAmount = timertime / diviseur;
            RightBarTimer.fillAmount = timertime / diviseur;
            timerText.text = timertime.ToString("0");
        }
        if (timertime < 0f && !gamemanager.gameOver)
            gamemanager.EndGame();
    }
}