
using System;
using TMPro;
using UnityEngine;


namespace ReadInputNameTag
{
    public class ReadInputNameTag : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField1;

        public static string input1;

        // Event when either input updates
        public static event Action<string, string> OnInputsChanged;

        // Called when InputField1 is updated
        public void ReadInput1()
        {
            string s1 = inputField1.text;

            if (string.IsNullOrWhiteSpace(s1))
            {
                Debug.LogWarning("Input field 1 is empty!");
                return;
            }

            input1 = s1;

            Debug.Log($"Input1: {input1}");
            
        }

    }
}


