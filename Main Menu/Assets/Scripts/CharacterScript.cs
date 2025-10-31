using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public DragonGrowthControl dragonControl;

    void OnCollectGem()
    {
        dragonControl.AddProgress(0.2f);
    }
}
