using UnityEngine;

public class SimpleUI : MonoBehaviour
{
      public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}