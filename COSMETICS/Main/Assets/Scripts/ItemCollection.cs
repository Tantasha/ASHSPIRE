using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public GrowthBarControl growthBarControl; // Reference to the GrowthBarControl script

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(growthBarControl != null)
            {
                growthBarControl.AddGrowth();
                Destroy(gameObject); // Destroy the item after collection

            }
        }

    }
}
