using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class QuestRewardSlot : MonoBehaviour
{
   public Image rewardImage;
   public TMP_Text rewardQuantity;

   public void DisplayReward(Sprite sprite, int quantity)
   {
        rewardImage.sprite = sprite;
        rewardQuantity.text = quantity.ToString();
   }
}
