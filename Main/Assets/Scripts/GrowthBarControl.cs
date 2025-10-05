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
    public float growthPerGem = 20f; // Growth increase per gem collected
    private float growthValue = 0f;

    public Transform dragonTransform;
    public float growthScale = 0.2f;
    public int gemToGrow = 2; // Number of gems required to grow
    public int gemCount = 0; // Current gem count
    public int growthStage = 0; // Current growth stage
    public int maxGrowthStages = 5; // Maximum number of growth stages

    public SpriteRenderer dragonRenderer;
    public Sprite teenDragon;

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
                new Color32(255, 165, 0, 255)    // Orange

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
        if(colourTransition)
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

            if (growthStage == maxGrowthStages && dragonRenderer != null && teenDragon != null)
            {
                dragonRenderer.sprite = teenDragon;
            }
        }
    }

    Color[] FilterPalette(Color[] originalPalette)
    {
        return System.Array.FindAll(originalPalette, color => color != Color.white && color != Color.black);

    }

}
