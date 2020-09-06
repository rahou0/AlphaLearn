using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelTrigger : MonoBehaviour
{
    int type = 0;
    Button button;
    ViewPanel viewPAnel;
    GameObject gobjectH;
    Animator animator;
    ManageSystemHand msysHand;
    // Start is called before the first frame update
    void Start()
    {
        viewPAnel = GameObject.Find("ViewPanel").GetComponent<ViewPanel>();
        button = gameObject.GetComponent<Button>();
        animator = gameObject.GetComponent<Animator>();
        msysHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type != 0 && viewPAnel.currentPanel == gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.name && !viewPAnel.isClicked)
        {
            if (type == 1)
            {
                if (!msysHand.IsOpenL && gobjectH.GetComponent<HandTrigger>().collidedobject == name)
                {
                    switch (name)
                    {
                        case "Back Reversed":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "Yes":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "No":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "Glock":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "MP5 SD":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "AKM":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "M4A1":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "Example One":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Two":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Three":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Four":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        default:
                            Debug.Log("aw hna");
                            viewPAnel.currentPanel = name + " Panel";
                            break;

                    }
                    viewPAnel.MakeAction();
                    animator.SetBool("Normal", true);
                    type = 0;
                    button.onClick.Invoke();
                }
            }
            else
            {
                if (!msysHand.IsOpenR && gobjectH.GetComponent<HandTrigger>().collidedobject == name)
                {
                    switch (name)
                    {
                        case "Back Reversed":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "Yes":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "No":
                            viewPAnel.currentPanel = "Main Panel";
                            break;
                        case "Glock":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "MP5 SD":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "AKM":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "M4A1":
                            viewPAnel.currentPanel = "Armory Panel";
                            break;
                        case "Example One":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Two":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Three":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        case "Example Four":
                            viewPAnel.currentPanel = "Mission Panel";
                            break;
                        default:
                            viewPAnel.currentPanel = name + " Panel";
                            break;
                    }
                    viewPAnel.MakeAction();
                    animator.SetBool("Normal", true);
                    type = 0;
                    button.onClick.Invoke();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "handR":
                gobjectH = other.gameObject;
                gobjectH.GetComponent<HandTrigger>().collidedobject = name;
                animator.SetBool("Highlighted", true);
                type = 2;
                break;
            case "handL":
                gobjectH = other.gameObject;
                gobjectH.GetComponent<HandTrigger>().collidedobject = name;
                animator.SetBool("Highlighted", true);
                type = 1;
                break;
            default:
                type = 0;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool("Normal", true);
    }

}
