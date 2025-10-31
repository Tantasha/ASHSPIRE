using UnityEngine;
using UnityEngine.UI;

public class DragonGrowthControl : MonoBehaviour
{
    //Dragon
    public GameObject babyDragon;
    public GameObject teenDragon;
    public GameObject adultDragon;

    //UI reference
    public Slider progressBar;
    public Image fillImage;

    //level colours
    public Color[] growthColor;

    //progress setting
    public float progress = 0f;
    public int currentGrowth = 0;
    public float progressPerGrowth = 1f;
    private GameObject currentDragon;

    void Start()
    {
        SpawnDragon(babyDragon);
        UpdateProgressBar();
    }

    public void AddProgress(float amount)
    {
        progress += amount;
        if (progress >= progressPerGrowth)
        {
            progress = 0f;
            currentGrowth++;
            HandleGrowUp();
        }
        UpdateProgressBar();
    }

    void HandleGrowUp()
    {
        if (currentGrowth == 10)
        {
            ReplaceDragon(teenDragon);
        }
        if (currentGrowth == 50)
        {
            ReplaceDragon(adultDragon);
        }
        if (currentGrowth < growthColor.Length)
        {
            fillImage.color = growthColor[currentGrowth];
        }

    }

    void UpdateProgressBar()
    {
        progressBar.value = progress / progressPerGrowth;
    }

    void SpawnDragon(GameObject dragon)
    {
        currentDragon = Instantiate(dragon, transform.position, Quaternion.identity);
    }

    void ReplaceDragon(GameObject newDragon)
    {
        Destroy(currentDragon);
        SpawnDragon(newDragon);
    }

}
