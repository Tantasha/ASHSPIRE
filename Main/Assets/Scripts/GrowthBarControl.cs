using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GrowthBarControl : MonoBehaviour
{
    //UI references
    public Slider growthBar;
    public Image fillImage;

    //Growth Settings
    public float growthSpeed = 0.5f; // Speed at which the growth bar fills
    public float growthPerGem = 50f; // Growth increase per gem collected
    private float growthValue = 0f;


    //Leveling Settings
    public int dragonLevel = 1; // Current dragon level
    public int maxDragonLevel = 5; // Maximum dragon level
    public int currentXP = 0; // Current experience points
    public int xpPerGem = 2;
    public int xpToNextLevel = 100; // Experience points needed to reach the next level


    //Transdorm and Scaling
    public Transform dragonTransform;
    public float growthScale = 0.2f;
    public int gemToGrow = 2; // Number of gems required to grow
    public int gemCount = 0; // Current gem count
    public int growthStage = 0; // Current growth stage
    public int maxGrowthStages = 2; // Maximum number of growth stages

    public SpriteRenderer dragonRenderer; //Dragon in the status panel
    public SpriteRenderer dragonInGame; //Dragon in the game play scene
    public Sprite teenDragon;
    public float teenSizeBoost = 0.2f; // Size boost when reaching teen stage

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

    void Start()
    {
        //Debug
        if (growthBar == null || fillImage == null)
        {
            Debug.LogError("GrowthBar or FillImage is not assigned in the inspector.");
            return;
        }

        growthBar.value = 0f;

        if (palette == null || palette.Length == 0)
        {
            palette = new Color[]
            {
                new Color32(255, 102, 178, 255), // Pink
                new Color32(153, 255, 51, 255),  // Lime Green
                new Color32(0, 255, 255, 255), // Cyan
                new Color32(128, 0, 255, 255),   // Purple
                new Color32(100, 149, 237, 255),   // blue
                new Color32(255, 165, 0, 255),     // Orange
                new Color32(255, 255, 0, 255),      // Yellow
                new Color32(173, 216, 230, 255),  // Light Blue
                new Color32(34, 139, 34, 255),   // Forest Green
                new Color32(255, 20, 147, 255), // Deep Pink
                new Color (178,34,34,255)  // Firebrick

            };
        }

        //Remove white and black from the palette if they exist
        palette = FilterPalette(palette);

        //Fallback if palette is empty after filtering
        if (palette.Length == 0)
        {
            palette = new Color[] { Color.gray };
            Debug.LogWarning("Palette is empty after filtering. Defaulting to gray.");
        }

        //set initial colour
        fillImage.color = palette[Mathf.Clamp(paletteIndex, 0, palette.Length - 1)];

    }

    void Update()
    {
        if (colourTransition)
        {
            colourTransitionTime += Time.deltaTime;
            float time = Mathf.Clamp01(colourTransitionTime / colourTransitionDuration);
            fillImage.color = Color.Lerp(startColour, targetColour, time);

            if (time >= 1f)
            {
                colourTransition = false;
            }
        }
    }

    public void AddGrowth()
    {
        if (growthBar == null || fillImage == null || palette.Length == 0) return;

        growthValue += growthPerGem;
        growthBar.value = Mathf.Clamp01(growthValue / 100f);
        gemCount++;
        currentXP += xpPerGem;

        if (currentXP >= xpToNextLevel && dragonLevel < maxDragonLevel)
        {
            currentXP -= xpToNextLevel;
            dragonLevel++;
            OnLevelUp();
        }

        if (gemCount >= gemToGrow)
        {
            gemCount = 0;
            growthValue = 0f;
            growthBar.value = 0f;

            if (growthStage < maxGrowthStages)
            {
                growthStage++;
                //Change dragon sprite at teen stage
                if (dragonTransform != null)
                {
                    Vector3 newScale = dragonTransform.localScale + new Vector3(growthScale, growthScale, 0f);
                    dragonTransform.localScale = newScale;
                }



                if (colour)
                {
                    paletteIndex = (paletteIndex + 1) % palette.Length;
                    targetColour = palette[paletteIndex];

                    startColour = fillImage.color;
                    colourTransitionTime = 0f;
                    colourTransition = true;
                }
            }

            if (growthStage == maxGrowthStages)
            {
                TransformToTeenDragon();
            }
        }
    }
    
    void TransformToTeenDragon()
    {
        if (teenDragon == null) return;
        if (dragonRenderer != null)
        {
            MatchSpriteSize(dragonRenderer, dragonRenderer.sprite, teenDragon);
            dragonTransform.localScale *= teenSizeBoost;
        }

        if(dragonInGame != null)
        {
            MatchSpriteSize(dragonInGame, dragonInGame.sprite, teenDragon);
            dragonInGame.transform.localScale *= teenSizeBoost;
        }    }


    void MatchSpriteSize(SpriteRenderer renderer, Sprite original, Sprite replacement)
    {
        if (original == null || replacement == null || renderer == null) return;
        float originalWidth = original.bounds.size.x;
        float replacementWidth = replacement.bounds.size.x;

        float scaleFactor = originalWidth / replacementWidth;
        renderer.sprite = replacement;
        renderer.transform.localScale *= scaleFactor;
    }

    Color[] FilterPalette(Color[] originalPalette)
    {
        return System.Array.FindAll(originalPalette, color => color != Color.white && color != Color.black);

    }

    void OnLevelUp()
    {
        Debug.Log("Dragon leveled up to level " + dragonLevel);

        if (dragonTransform != null)
        {
            dragonTransform.localScale += new Vector3(growthScale, growthScale, 0f);
        }

        if (dragonLevel == 2)
        {
            TransformToTeenDragon ();
        }
    }
}
