using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public GrowthBarControl growthBarControl; // Reference to the GrowthBarControl script
    public bool isGem = false;  //Reference to whether the item is a gem or food
    public BiomeType foodBiome; 


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && growthBarControl != null)
        {
            if(isGem)
            {
                growthBarControl.AddGrowthFromGem();
            }
            else
            {
                growthBarControl.AddGrowthFromFood(foodBiome);
            }
            Destroy(gameObject); // Destroy the item after collection
        }

    }
}
