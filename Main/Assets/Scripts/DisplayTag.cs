

using UnityEngine;
using TMPro;

namespace ReadInputNameTag
{
    public class DisplayTag : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI textElement;
        private string NameTag;

        private void Update()
        {
            if (ReadInputNameTag.input1 != NameTag)
            {
                NameTag = ReadInputNameTag.input1;
                textElement.text = NameTag;
            }
        }

    }
}
