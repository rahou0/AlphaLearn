using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    GameObject button;
    public string collidedobject = "";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Button")
        {
            button = other.gameObject;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Button")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Normal", true);
        }
    }
    */
}
