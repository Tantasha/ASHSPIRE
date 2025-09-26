using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public DragonGrowthControl growthControl;
    public float progressPerGem = 0.5f;
    public float progressPerFood = 0.5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered with: " + collision.name);
        if (collision.CompareTag("Gem") || collision.CompareTag("Food"))
        {
            growthControl.AddProgress(progressPerGem);
            Destroy(collision.gameObject);
        }
    }
}
