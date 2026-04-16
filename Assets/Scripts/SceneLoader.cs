using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;



public class SceneLoader : MonoBehaviour
{



void Awake()
{
   
}

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
 
    
}
