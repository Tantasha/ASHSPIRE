using UnityEngine;
using UnityEngine.UI;

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

    public void Start()
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

    public void SetProgress(float value)
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

