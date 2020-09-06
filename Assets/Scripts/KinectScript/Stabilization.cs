using UnityEngine;

public class Stabilization : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.x != 0 || transform.rotation.y != 0 || transform.rotation.z != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
