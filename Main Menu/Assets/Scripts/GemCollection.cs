using UnityEngine;

public class GemCollection : MonoBehaviour
{
    public DragonGrowthControl growthControl;
    public float progressPerGem = 0.2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered with: " + collision.name);
        if (collision.CompareTag("Gem"))
        {
            growthControl.AddProgress(progressPerGem);
            Destroy(collision.gameObject);
        }
    }
}
