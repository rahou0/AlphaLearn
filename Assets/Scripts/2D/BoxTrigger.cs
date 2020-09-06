using UnityEngine.UI;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
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
        if (type != 0 && viewPAnel.currentPanel == gameObject.transform.parent.gameObject.name && !viewPAnel.isClicked)
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
