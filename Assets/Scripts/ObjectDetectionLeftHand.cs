using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectDetectionLeftHand : MonoBehaviour
{
    public SphereCollider sphCol;
    ManageSystemHand myHand;
    public bool isSelected = false;
    public bool isArrived = false;
    public GameObject gameObjectSelected=null;
    // Start is called before the first frame update
    void Start()
    {
        myHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
        gameObjectSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && !myHand.IsOpenL)
        {
            gameObjectSelected.transform.position = transform.TransformPoint(sphCol.center);
            if (gameObjectSelected.transform.rotation.x != 0 || gameObjectSelected.transform.rotation.y != 0 || gameObjectSelected.transform.rotation.z != 0)
                gameObjectSelected.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "box")
            {
            if (GameObject.Find("GamePauseUI") == null && GameObject.Find("GameExitUI") == null)
            {
                FindObjectOfType<AudioManagerSystem>().PlaySound(other.gameObject.name.Replace("Cube ", ""));
                other.gameObject.GetComponent<CubesController>().hand = 1;
                gameObjectSelected = other.gameObject;
                AddGlow(other.gameObject);
                isSelected = true;
            }
        }

       
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            other.gameObject.GetComponent<CubesController>().hand = 0;
            isSelected = false;
            RemoveGlow(other.gameObject);
            gameObjectSelected = null;
        }
    }
    #region add and remove glow effect to boxes
    void AddGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        //gObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    void RemoveGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        //gObject.GetComponent<Rigidbody>().isKinematic = false;

    }
    #endregion
}
