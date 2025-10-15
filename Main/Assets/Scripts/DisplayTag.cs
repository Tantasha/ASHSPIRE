

using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace ReadInputNameTag
{
    public class DisplayTag : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI textElement;
        private static List<DisplayTag> allDisplays = new List<DisplayTag>();


        private void Awake()
        {
            // Register this display
            allDisplays.Add(this);
        }


        private void OnDestroy()
        {
            // Unregister when destroyed
            allDisplays.Remove(this);
        }



        private void Start()
        {
            // Load and display saved name immediately
            if (PlayerPrefs.HasKey("SavedNameTag"))
            {
                string savedName = PlayerPrefs.GetString("SavedNameTag");
                textElement.text = savedName;
            }
            else
            {
                textElement.text = "";
            }
        }

        // Called when any DisplayTag needs to refresh
        public static void UpdateAllDisplays()
        {
            string latestName = PlayerPrefs.GetString("SavedNameTag", "");
            foreach (var display in allDisplays)
            {
                if (display.textElement != null)
                    display.textElement.text = latestName;
            }
        }

    }
}
