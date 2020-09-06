using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateHAnds : MonoBehaviour
{
    public GameObject closed;
    public GameObject Opened;
    ManageSystemHand myHand;
    void Start()
    {
        myHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
        closed.SetActive(false);
    }
    void Update()
    {
        switch (name.ToString())
        {
            case "HandRight":
                if (myHand.IsOpenR && closed.activeSelf)
                {
                    closed.SetActive(false);
                    Opened.SetActive(true);
                }
                if (!myHand.IsOpenR && Opened.activeSelf)
                {
                    closed.SetActive(true);
                    Opened.SetActive(false);
                }
                break;
            case "HandLeft":
                if (myHand.IsOpenL && closed.activeSelf)
                {
                    closed.SetActive(false);
                    Opened.SetActive(true);
                }
                if (!myHand.IsOpenL && Opened.activeSelf)
                {
                    closed.SetActive(true);
                    Opened.SetActive(false);
                }
                break;
        }
        
    }
}
