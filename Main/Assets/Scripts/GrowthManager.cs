using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    [SerializeField] private GrowthBarControl growthBarControl;
    [SerializeField] private DragonStats stats;
    [SerializeField] private DragonVisual visuals;

    [SerializeField] private float growthPerGem = 20f;
    [SerializeField] private float growthPerFood = 30f;
    [SerializeField] private int gemThreshold = 4;
    [SerializeField] private int foodThreshold = 2;
    [SerializeField] private int xpPerGem = 2;
    [SerializeField] private int xpPerFood = 10;
    [SerializeField] private float growthScale = 0.2f;

    [SerializeField] private BiomeType currentBiome;
    private BiomeType dragonType;
    private float growthValue;
    private int gemCount;
    private int foodCount;

    public void AddGem()
    {
        growthValue += growthPerGem;
        gemCount++;
        stats.AddXP(xpPerGem);
        growthBarControl.SetProgress(growthValue);
        TryGrow();
    }

    public void AddFood(BiomeType foodBiome)
    {
        if (foodBiome != currentBiome) return;
        growthValue += growthPerFood;
        foodCount++;
        stats.AddXP(xpPerFood);
        growthBarControl.SetProgress(growthValue);
        TryGrow();

        if (dragonType != foodBiome)
        {
            dragonType = foodBiome;
            visuals.TransformToBiome(foodBiome);
        }
    }

    private void TryGrow()
    {
        if (gemCount >= gemThreshold || foodCount >= foodThreshold)
        {
            gemCount = 0;
            foodCount = 0;
            growthValue = 0f;
            growthBarControl.SetProgress(0f);

            if (stats.TryGrow())
            {
                visuals.ScaleDragon(growthScale);
                growthBarControl.AdvanceColor();
            }

            if (stats.IsTeen)
                visuals.TransformToTeen(stats.DragonFormBiome);
        }
    }
}

