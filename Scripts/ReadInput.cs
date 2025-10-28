using System;
using TMPro;
using UnityEngine;

namespace ReadInput
{
    public class ReadInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField1;
        [SerializeField] private TMP_InputField inputField2;

        public static string input1;
        public static string input2;

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

            OnInputsChanged?.Invoke(input1, input2);
        }
        
        public void ReadInput2()
        {
            string s2 = inputField2.text;
            if (string.IsNullOrWhiteSpace(s2))
            {
                Debug.LogWarning("Input field 2 is empty!");
                return;
            }

            input2 = s2;
            Debug.Log($"Input2: {input2}");

            OnInputsChanged?.Invoke(input1, input2);
        }
    }
}
