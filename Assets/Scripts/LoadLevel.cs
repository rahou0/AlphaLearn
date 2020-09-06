using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    SearchIndex sindex;
    void Start()
    {
        sindex = GameObject.Find("SearchIndex").GetComponent<SearchIndex>();
    }
    public void LoadNewLEvel()
    {
        //sindex.AddLevelAction();
        if (sindex.actionsUI[SceneManager.GetActiveScene().name].indice==6)
            SceneManager.LoadScene("Home");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
