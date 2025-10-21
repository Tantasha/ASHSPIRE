
using System;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;


namespace ReadInputNameTag
{
    public class ReadInputNameTag : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField1;
        [SerializeField] private TextMeshProUGUI errorText; // UI text for error message


        public static string input1;

        private const string PlayerPrefKey = "SavedNameTag"; // Key to store input in PlayerPrefs

        private void Start()
        {

            // Hide error text at start
            if (errorText != null)
                errorText.text = "";

            // Load saved input (if any)
            if (PlayerPrefs.HasKey(PlayerPrefKey))
            {
                input1 = PlayerPrefs.GetString(PlayerPrefKey);
                inputField1.text = input1; // Display saved value in the InputField
                Debug.Log($"Loaded saved name: {input1}");
            }
        } 

        // Called when InputField1 is updated
        public void ReadInput1()
        {
            string s1 = inputField1.text;

            if (string.IsNullOrWhiteSpace(s1))
            {
                Debug.LogWarning("Input field 1 is empty!");
                return;
            }


            // Validate input: only letters and spaces allowed
            if (!Regex.IsMatch(s1, @"^[a-zA-Z\s]+$"))
            {
                ShowError(" Only letters and spaces are allowed!");
                return;
            }

            // Clear any previous error
            ShowError("");


            input1 = s1;


            // Save the value permanently
            PlayerPrefs.SetString(PlayerPrefKey, input1);
            PlayerPrefs.Save();

            // Notify other canvases (DisplayTag scripts)
            DisplayTag.UpdateAllDisplays();

            Debug.Log($"Input1: {input1}");
            
        }


        private void ShowError(string message)
        {
            if (errorText != null)
                errorText.text = message;

            if (!string.IsNullOrEmpty(message))
                Debug.LogWarning(message);
        }

    }
}


