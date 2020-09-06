using UnityEngine;
public class ObjectDetectionRightHand : MonoBehaviour
{
    public SphereCollider sphCol;
    ManageSystemHand myHand;
    public bool isSelected = false;
    public bool isArrived = false;
    public GameObject gameObjectSelected;
    GameObject gObj;
    // Start is called before the first frame update
    void Start()
    {
        myHand = GameObject.Find("ManagerSysHand").GetComponent<ManageSystemHand>();
        gameObjectSelected = null;
    }

    // Update is called once per frame
    void Update()
    {
        //case of witch the hand is closed and object(cube) is selected
        if (isSelected && !myHand.IsOpenR)
        {
            //transform the position of the cube with the position of the hand
            gameObjectSelected.transform.position = transform.TransformPoint(sphCol.center);

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
            if (GameObject.Find("GamePauseUI") == null && GameObject.Find("GameExitUI") == null)
            {
                FindObjectOfType<AudioManagerSystem>().PlaySound(other.gameObject.name.Replace("Cube ", ""));
                other.gameObject.GetComponent<CubesController>().hand = 2;
                gameObjectSelected = other.gameObject;
                AddGlow(other.gameObject);
                isSelected = true;
            }
        }
    }
    //detect ki yji kharej man collision
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "box")
        {
            other.gameObject.GetComponent<CubesController>().hand = 0;
            gameObjectSelected = null;
            isSelected = false;
            RemoveGlow(other.gameObject);
        }
    }
    #region add and remove glow effect to boxes
    void AddGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
    void RemoveGlow(GameObject gObject)
    {
        gObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
    #endregion
}
