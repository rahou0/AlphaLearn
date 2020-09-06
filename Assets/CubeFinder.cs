using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -14 || transform.position.x > 0 || transform.position.y < -0.5 || transform.position.z < 0 || transform.position.z > 14)
            transform.position = new Vector3(-7,3,8);
    }
}
