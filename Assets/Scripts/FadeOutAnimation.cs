using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;



public class FadeOutAnimation : MonoBehaviour
{

public float fadeDuration = 1f;

private CanvasGroup canvasGroup;

void Awake()
{
    canvasGroup = GetComponent<CanvasGroup>();
     StartCoroutine(FadeOut());
}

 IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(1f); // Optional delay before starting fade-out
        float time = 0;

        while (time < fadeDuration) 
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
    
}
