using UnityEngine;
using TMPro;

namespace ReadInput
{
    public class ConfirmReport : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI textElement;
        private string _last;

        private void Update()
        {
            if (ReadInput.input1  != _last)
            {
                _last = ReadInput.input1;
                textElement.text = "The following report has been saved: (TITLE: " + _last+")";
            }
        }

    }
}

