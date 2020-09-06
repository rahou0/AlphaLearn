using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPanel : MonoBehaviour
{
    public string currentPanel;
    public bool isClicked = false; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeAction()
    {
        StartCoroutine(ExecuteAfterTime(1f));
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        isClicked = true;
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        isClicked = false;
    }
}
