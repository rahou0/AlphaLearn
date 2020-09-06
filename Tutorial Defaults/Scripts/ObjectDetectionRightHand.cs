using UnityEngine;
public class ObjectDetectionRightHand : MonoBehaviour
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
        //case of witch the hand is closed and object(cube) is selected
        if (isSelected && !myHand.IsOpenR)
        {
            //transform the position of the cube with the position of the hand
            gameObjectSelected.transform.position = transform.position;

            //the if is used to keep the cube in the uniform orientation
            if (gameObjectSelected.transform.rotation.x != 0 || gameObjectSelected.transform.rotation.y != 0 || gameObjectSelected.transform.rotation.z != 0)
                gameObjectSelected.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    //detect collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            gameObjectSelected = other.gameObject;
            other.gameObject.GetComponent<Renderer>().material.color = new Color32(190, 87, 101, 255);
            isSelected = true;
        }
    }
    //detect ki yji kharej man collision
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            isSelected = false;
            other.gameObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }
    }
}
