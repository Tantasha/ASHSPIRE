using UnityEngine;

public class CanvasPanel : MonoBehaviour
{
    CanvasGroup canvasPanel;

    void HidePanel()
    {
        canvasPanel.alpha = 0f; // Make the panel invisible
        canvasPanel.interactable = false; // Disable interaction
        canvasPanel.blocksRaycasts = false; // Disable raycast blocking
    }

    void ShowPanel()
    {
        canvasPanel.alpha = 1f; // Make the panel visible
        canvasPanel.interactable = true; // Enable interaction
        canvasPanel.blocksRaycasts = true; // Enable raycast blocking
    }
}
