using UnityEngine;
using TMPro;

namespace ReadInput
{

    public class ClearInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField myInputField1;
        [SerializeField] private TMP_InputField myInputField2;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void Clear()
        {
            myInputField1.text = "";
            myInputField2.text = "";

            ReadInput.input1 = "";
            ReadInput.input2 = "";
        }

    }
}


