using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManagerSystem").GetComponent<GameManagerSystem>().isenabled)
        {
            GameObject.Find("Main Camera (1)").transform.position = transform.position;
            GameObject.Find("Main Camera (1)").transform.rotation = Quaternion.Euler(transform.position.y * 2 + (-transform.position.z + 40) * 2, 180 + transform.position.x * 2, GameObject.Find("Main Camera (1)").transform.rotation.z);
        }
    }
}
