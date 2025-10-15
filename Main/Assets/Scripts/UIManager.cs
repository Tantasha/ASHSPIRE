using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Drag and drop the ShopInterfacePanel GameObject onto this slot in the Inspector
    public GameObject shopPanel;

    public void ToggleShopPanel()
    {
        if (shopPanel != null)
        {
            // 1. Toggle the visual state of the shop panel
            bool isCurrentlyActive = shopPanel.activeSelf;
            shopPanel.SetActive(!isCurrentlyActive);

            // 2. Control the game flow (Time Scale)
            if (!isCurrentlyActive)
            {
                // Shop is opening (was inactive)
                // Set Time.timeScale to 0 to pause all time-based game processes
                Time.timeScale = 0f;

                // You may also want to unlock the cursor if it's a PC game
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Shop is closing (was active)
                // Set Time.timeScale back to 1 to resume normal speed
                Time.timeScale = 1f;

                // Lock the cursor and hide it again for free-roam gameplay
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Debug.LogError("Shop Panel is not assigned to the UIManager script!");
        }
    }
}
