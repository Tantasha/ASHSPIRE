using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] private GrowthManager growthManager;
    [SerializeField] private bool isGem;
    [SerializeField] private BiomeType foodBiome;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || growthManager == null) return;

        if (isGem)
            growthManager.AddGem();
        else
            growthManager.AddFood(foodBiome);

        Destroy(gameObject);
    }
}
