using UnityEngine;

public class AdjustUIForHeight : MonoBehaviour
{
    RectTransform targetImage;
    float lastHeight;

    void Awake()
    {
        targetImage = GetComponent<RectTransform>();
    }

    void Start()
    {
        Adjust();
    }

    void Update()
    {
        // Only update if screen size changes (rotation / resize)
        if (Screen.height != lastHeight)
        {
            Adjust();
        }
    }

    void Adjust()
    {
        float screenHeight = Screen.height;
        lastHeight = screenHeight;

        if (screenHeight > 1920f)
        {
            targetImage.offsetMin = new Vector2(targetImage.offsetMin.x, 25f);
            targetImage.offsetMax = new Vector2(targetImage.offsetMax.x, -25f);
        }
        else
        {
            targetImage.offsetMin = new Vector2(targetImage.offsetMin.x, 0f);
            targetImage.offsetMax = new Vector2(targetImage.offsetMax.x, 0f);
        }
    }
}