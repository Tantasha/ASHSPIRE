using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the visual state and interaction logic for an individual shop item.
/// </summary>
public class ShopItem : MonoBehaviour
{
    [Tooltip("The name of the scene/environment this item belongs to (e.g., 'Forest', 'Volcano').")]
    public string RequiredEnvironment;

    [Tooltip("The Button component on this GameObject.")]
    public Button itemButton;

    [Tooltip("The Image component to dim (usually the main icon/image).")]
    public Image itemImage;

    [Header("Cosmetic Settings")]
    [Tooltip("The color used when the item is dimmed (e.g., light gray).")]
    public Color dimmedColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    private Color originalColor;

    private void Awake()
    {
        // Get the required components
        if (itemButton == null)
            itemButton = GetComponent<Button>();

        if (itemImage == null)
            itemImage = GetComponent<Image>();

        if (itemImage != null)
            originalColor = itemImage.color;

        // Add listener for the custom dialogue box logic (since alerts are forbidden)
        // Ensure the button is clickable even when dimmed to show the warning.
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(OnItemClicked);
        }
    }

    /// <summary>
    /// Updates the item's visual state based on the current scene.
    /// </summary>
    /// <param name="currentScene">The name of the currently active environment scene.</param>
    public void UpdateState(string currentScene)
    {
        bool matchesEnvironment = currentScene.Equals(RequiredEnvironment, System.StringComparison.OrdinalIgnoreCase);

        if (itemImage != null)
        {
            // Dim or undim the item visually
            itemImage.color = matchesEnvironment ? originalColor : dimmedColor;
        }

        // We keep the button interactable but use the click handler to determine the action
        // itemButton.interactable = true; 
    }

    /// <summary>
    /// Logic executed when the item button is pressed.
    /// </summary>
    private void OnItemClicked()
    {
        if (GameSceneManager.Instance == null) return;

        string currentScene = GameSceneManager.Instance.CurrentEnvironmentSceneName;
        bool matchesEnvironment = currentScene.Equals(RequiredEnvironment, System.StringComparison.OrdinalIgnoreCase);

        if (matchesEnvironment)
        {
            Debug.Log($"Purchasing item: {gameObject.name}...");
            // TODO: Add actual purchase logic here
        }
        else
        {
            // Show the warning dialogue box
            // Since we can't use built-in Unity dialogue, we'll call a method on the UIManager.
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowEnvironmentWarningDialogue(RequiredEnvironment);
            }
        }
    }
}

