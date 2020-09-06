using UnityEngine;

public class CubesController : MonoBehaviour
{
    public int hand = 0;
    void Update()
    {
        if (transform.position.x >= 15)
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        if (transform.position.x <= -15)
            transform.position = new Vector3(-9, transform.position.y, transform.position.z);
        if (transform.position.z >= 40)
            transform.position = new Vector3(transform.position.x, transform.position.y, 26);
        if (transform.position.z <= 15)
            transform.position = new Vector3(transform.position.x, transform.position.y, 15);
    }
}