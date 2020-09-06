using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectDetectionLeftHand : MonoBehaviour
{
    ManageSystemHand myHand;
    bool isSelected = false;
    GameObject gameObjectSelected;
    // Start is called before the first frame update
    void Start()
    {
        myHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && !myHand.IsOpenL)
        {
            gameObjectSelected.transform.position = transform.position;
            if (gameObjectSelected.transform.rotation.x != 0 || gameObjectSelected.transform.rotation.y != 0 || gameObjectSelected.transform.rotation.z != 0)
                gameObjectSelected.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "box")
        {
            gameObjectSelected = other.gameObject;
            other.gameObject.GetComponent<Renderer>().material.color = new Color32(190, 87, 101, 255);
            isSelected = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            isSelected = false;
            other.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }
    }
}
