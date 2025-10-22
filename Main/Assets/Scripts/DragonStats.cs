using UnityEngine;

public class DragonStats : MonoBehaviour
{
    //Progression Setting
    [SerializeField] private int maxLevel = 5;
    [SerializeField] private int xpToNextLevel = 100;
    //[SerializeField] private float growthScale = 0.1f;

    //Biome Tracking 
    [SerializeField] private BiomeType startingBiome;
    public BiomeType CurrentBiome { get; private set; }
    public BiomeType DragonFormBiome { get; private set; }

    //Evolution stage
    public bool IsTeen => GrowthStage >= MaxGrowthStages;
    public bool IsAdult => Level >= maxLevel;

    public int Level { get; private set; } = 1;
    public int XP { get; private set; }
    public int GrowthStage { get; private set; }
    public int MaxGrowthStages => 2;

    // inistialise biome form and current biome
    private void Awake()
    {
        
            CurrentBiome = startingBiome;
            DragonFormBiome = startingBiome;
    }

    public void SetCurrentBiome(BiomeType biome)
    {
        CurrentBiome = biome;
    }

    public void AddXP(int amount)
    {
        XP += amount;
        if (XP >= xpToNextLevel && Level < maxLevel)
        {
            XP -= xpToNextLevel;
            Level++;
        }
    }

    public bool TryGrow()
    {
        if (GrowthStage < MaxGrowthStages)
        {
            GrowthStage++;
            return true;
        }
        return false;
    }

    public bool ShouldTransformToNewBiome(BiomeType itemBiome)
    {
        return itemBiome != DragonFormBiome;
    }

    public void TransformDragonToBiome(BiomeType newBiome)
    {
        DragonFormBiome = newBiome;
    }
    
    
}
