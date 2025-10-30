using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum BiomeType
{
    Forest,
    Desert,
    Snow,
    Volcano
}

public class GrowthBarControl : MonoBehaviour
{
    [SerializeField] private Slider growthBar;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color[] palette;
    [SerializeField] private float transitionDuration = 0.5f;

    private int paletteIndex;
    private bool isTransitioning;
    private Color startColor;
    private Color targetColor;
    private float transitionTime;


    //Leveling Settings
    public int dragonLevel = 1; // Current dragon level
    public int maxDragonLevel = 5; // Maximum dragon level
    public int currentXP = 0; // Current experience points
    public int xpPerGem = 2;
    public int xpPerFood = 10;
    public int xpToNextLevel = 100; // Experience points needed to reach the next level


    //Transdorm and Scaling
    public Transform dragonTransform;
    public float growthScale = 0.1f;
    public int gemToGrow = 4; // Number of gems required to grow
    public int foodToGrow = 2; // Number of food items required to grow
    public int gemCount = 0; // Current gem count
    public int foodCount = 0; // Current food count
    public int growthStage = 0; // Current growth stage
    public int maxGrowthStages = 2; // Maximum number of growth stages

    public SpriteRenderer dragonRenderer; //Dragon in the status panel
    public SpriteRenderer dragonInGame; //Dragon in the game play scene
    public Sprite teenDragon;
    public float teenSizeBoost = 0.7f; // Size boost when reaching teen stage

    //Colour Settings
    public bool colour = true; // Toggle for colors
    public Color[] palette; //custom color palette
    private int paletteIndex = 0; //tracks the current color index

    //Colour transition settings
    private bool colourTransition = false;
    private Color startColour;
    private Color targetColour;
    private float colourTransitionTime = 0f;
    private float colourTransitionDuration = 0.5f; // Duration of the color transition

    //Birome Tracking
    public BiomeType currentBiome;
    void Awake()
    {
        palette = FilterPalette(palette);
        paletteIndex = 0;

        if(palette.Length == 0)
        {
            Debug.LogWarning("Filtered palette is empty.");
            return;
        }
        fillImage.color = palette[paletteIndex];
    }

    public void Update()
    {
        if (isTransitioning)
        {
            transitionTime += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTime / transitionDuration);
            fillImage.color = Color.Lerp(startColor, targetColor, t);
            if (t >= 1f) isTransitioning = false;
        }
    }

    //set process
    public void SetProgress(float progress)
    {
        growthValue = Mathf.Clamp(progress, 0f, 100f);
        if (growthBar != null)
        {
            growthBar.value = growthValue / 100f;
        }
    }
    
    //advance colour manually
    public void AdvanceColor()
    {
        if (!colour || palette == null || palette.Length == 0 || fillImage == null) return;

        paletteIndex = (paletteIndex + 1) % palette.Length;
        targetColour = palette[paletteIndex];
        startColour = fillImage.color;
        colourTransitionTime = 0f;
        colourTransition = true;

    }

    // Check if the dragon can level up from collecting gems
    public void AddGrowthFromGem()
    {
        growthBar.value = Mathf.Clamp01(value / 100f);
    }

    public void AdvanceColor()
    {
        paletteIndex = (paletteIndex + 1) % palette.Length;
        startColor = fillImage.color;
        targetColor = palette[paletteIndex];
        transitionTime = 0f;
        isTransitioning = true;
    }

    private Color[] FilterPalette(Color[] original)
    {
        return System.Array.FindAll(original, c => c != Color.white && c != Color.black);
    }
}

