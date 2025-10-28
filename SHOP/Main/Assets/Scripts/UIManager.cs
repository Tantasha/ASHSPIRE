using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Make UIManager a Singleton for easy global access
    public static UIManager Instance { get; private set; }

    // --- EXISTING UI REFERENCES (MUST BE DRAGGED INTO THE INSPECTOR) ---
    public GameObject ShopInterfacePanel;
    public GameObject ShopIconButton;
    public GameObject OptionMenu;
    public Button OptionButtonComponent;
    public GameObject DimmerPanel;

    // --- NEW SHOP CONTENT REFERENCES ---
    [Header("Shop Content Panels")]
    // Drag the three content panel GameObjects here
    public GameObject SkillsContentPanel;
    public GameObject ResourcesContentPanel;
    public GameObject CosmeticsContentPanel;

    [Header("Shop Item Management")]
    [Tooltip("Reference to the simple dialogue box for warnings.")]
    public GameObject WarningDialoguePanel;
    [Tooltip("Reference to the Text component inside the warning dialogue.")]
    public Text WarningDialogueText;

    // Cache all shop items in the scene (found dynamically in Start)
    private ShopItem[] allCosmeticItems;


    private void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // 1. Find all ShopItem components in the scene
        allCosmeticItems = FindObjectsOfType<ShopItem>();

        // 2. Subscribe to the scene change event managed by GameSceneManager
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.OnEnvironmentSceneChanged += UpdateCosmeticItemsState;

            // Initial update for the current scene (in case the manager initialized first)
            UpdateCosmeticItemsState(GameSceneManager.Instance.CurrentEnvironmentSceneName);
        }
        else
        {
            Debug.LogError("GameSceneManager instance not found. Shop cosmetics will not update.");
        }

        // Ensure the warning dialogue is hidden at start
        if (WarningDialoguePanel != null)
        {
            WarningDialoguePanel.SetActive(false);
        }
    }

    /// <summary>
    /// Iterates through all cosmetic items and updates their dimming/interactivity state.
    /// This is called when the shop opens OR when the scene changes.
    /// </summary>
    private void UpdateCosmeticItemsState(string currentScene)
    {
        if (allCosmeticItems == null)
        {
            // Try finding them again if called early or if scene loads changed things
            allCosmeticItems = FindObjectsOfType<ShopItem>();
            if (allCosmeticItems.Length == 0) return;
        }

        foreach (ShopItem item in allCosmeticItems)
        {
            item.UpdateState(currentScene);
        }
    }

    // --------------------------------------------------------------------------------
    // 1. OPTIONS MENU FUNCTIONS
    // --------------------------------------------------------------------------------

    public void OpenOptionsPanel()
    {
        if (OptionMenu != null && ShopIconButton != null)
        {
            // State Control: Open Menu & Hide Other UI
            OptionMenu.SetActive(true);
            ShopIconButton.SetActive(false); // Hides the Shop Icon

            // Game Flow Control: Pause Game
            Time.timeScale = 0f;

            // Cursor Control
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogError("ERROR: OptionMenu or ShopIconButton reference is missing in the Inspector.");
        }
    }

    public void CloseOptionsPanel()
    {
        if (OptionMenu != null && ShopIconButton != null)
        {
            // State Control: Close Menu & Show Other UI
            OptionMenu.SetActive(false);
            ShopIconButton.SetActive(true); // Shows the Shop Icon again

            // Game Flow Control: Resume Game
            Time.timeScale = 1f;

            // Cursor Control
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // --------------------------------------------------------------------------------
    // 2. SHOP MENU FUNCTIONS (Toggle)
    // --------------------------------------------------------------------------------

    public void ToggleShopPanel()
    {
        if (ShopInterfacePanel != null && OptionButtonComponent != null && DimmerPanel != null)
        {
            // 1. Toggle the visual state of the shop panel
            bool isCurrentlyActive = ShopInterfacePanel.activeSelf;
            ShopInterfacePanel.SetActive(!isCurrentlyActive);

            // 2. Control Dimmer and Other UI
            DimmerPanel.SetActive(!isCurrentlyActive);

            // 3. Control the game flow (Time Scale)
            if (!isCurrentlyActive)
            {
                // Shop is opening
                OptionButtonComponent.interactable = false;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Ensure Resources is the default visible tab when the shop first opens
                ShowShopContentPanel(ResourcesContentPanel);

                // Update the cosmetic item states immediately when the shop opens
                if (GameSceneManager.Instance != null)
                {
                    UpdateCosmeticItemsState(GameSceneManager.Instance.CurrentEnvironmentSceneName);
                }
            }
            else
            {
                // Shop is closing
                OptionButtonComponent.interactable = true;
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Hide any potential warning dialogue upon closing the main shop
                CloseWarningDialogue();
            }
        }
        else
        {
            Debug.LogError("ERROR: One or more UI references are missing in UIManager.");
        }
    }

    // --------------------------------------------------------------------------------
    // 3. SHOP TAB SWITCHING LOGIC
    // --------------------------------------------------------------------------------

    /// <summary>
    /// Deactivates all content panels except the one specified to be shown.
    /// </summary>
    /// <param name="panelToShow">The GameObject of the panel to activate.</param>
    public void ShowShopContentPanel(GameObject panelToShow)
    {
        // Safety check to ensure all panels are assigned
        if (SkillsContentPanel == null || ResourcesContentPanel == null || CosmeticsContentPanel == null)
        {
            Debug.LogError("Shop content panels are not all assigned in the Inspector!");
            return;
        }

        // Deactivate all panels first
        SkillsContentPanel.SetActive(false);
        ResourcesContentPanel.SetActive(false);
        CosmeticsContentPanel.SetActive(false);

        // Activate the requested panel
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }
    }

    // --------------------------------------------------------------------------------
    // 4. DIALOGUE WARNING LOGIC
    // --------------------------------------------------------------------------------

    /// <summary>
    /// Shows a warning when a user tries to buy an item from a different environment.
    /// </summary>
    /// <param name="requiredEnvironment">The environment the user needs to be in.</param>
    public void ShowEnvironmentWarningDialogue(string requiredEnvironment)
    {
        if (WarningDialoguePanel != null && WarningDialogueText != null)
        {
            // Provide a clear message to the user
            WarningDialogueText.text = $"You must switch to the '{requiredEnvironment}' environment to purchase this item.";

            // Bring the dialogue panel to the front and make it visible
            WarningDialoguePanel.transform.SetAsLastSibling();
            WarningDialoguePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Warning Dialogue Panel or Text is not assigned in UIManager.");
        }
    }

    /// <summary>
    /// Function to close the warning dialogue, typically called by a button inside the dialogue box.
    /// </summary>
    public void CloseWarningDialogue()
    {
        if (WarningDialoguePanel != null)
        {
            WarningDialoguePanel.SetActive(false);
        }
    }
}