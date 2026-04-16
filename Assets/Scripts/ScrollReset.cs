using UnityEngine;
using UnityEngine.UI;

public class ScrollReset : MonoBehaviour
{
    private Scrollbar scrollbar;

    void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    void OnEnable()
    {
        Canvas.ForceUpdateCanvases();
        scrollbar.value = 0f;
    }
}