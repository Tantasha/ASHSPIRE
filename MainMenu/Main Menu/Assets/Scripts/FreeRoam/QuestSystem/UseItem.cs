using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        if(itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
