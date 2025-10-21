using Unity.VisualScripting;
using UnityEngine;

public class DragonVisual : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform dragonTransform;
    [SerializeField] private SpriteRenderer statusRenderer;
    [SerializeField] private SpriteRenderer inGameRenderer;

    [Header("Baby dragon Sprites")]
    [SerializeField] private Sprite forestSprite;
    [SerializeField] private Sprite snowSprite;
    [SerializeField] private Sprite desertSprite;
    [SerializeField] private Sprite volcanoSprite;


    [Header("Teen Dragon Sprite")]
    [SerializeField] private Sprite forestTeenSprite;
    [SerializeField] private Sprite snowTeenSprite;
    [SerializeField] private Sprite desertTeenSprite;
    [SerializeField] private Sprite volcanoTeenSprite;
    [SerializeField] private float teenSizeBoost = 0.7f;

    public void ScaleDragon(float amount)
    {
        if (dragonTransform != null)
            dragonTransform.localScale += new Vector3(amount, amount, 0f);
    }

    public void TransformToBiome(BiomeType biome)
    {
        Sprite biomeSprite = biome switch
        {
            BiomeType.Forest => forestSprite,
            BiomeType.Snow => snowSprite,
            BiomeType.Desert => desertSprite,
            BiomeType.Volcano => volcanoSprite,
            _ => null
        };

        if (biomeSprite != null)
            ApplySprite(biomeSprite, 1f);
    }

//Reference to the teen Dragon Sprite
    public void TransformToTeen(BiomeType biome)
    {
        Sprite teenBiomeSprite = biome switch
        {
            BiomeType.Forest => forestTeenSprite,
            BiomeType.Snow => snowTeenSprite,
            BiomeType.Desert => desertTeenSprite,
            BiomeType.Volcano => volcanoTeenSprite,
            _ => null
        };

        if (teenBiomeSprite != null)
            ApplySprite(teenBiomeSprite, teenSizeBoost);
    }

    private void ApplySprite(Sprite sprite, float scaleBoost)
    {
        if (statusRenderer != null)
        {
            MatchSize(statusRenderer, sprite);
            statusRenderer.transform.localScale *= scaleBoost;
        }

        if (inGameRenderer != null)
        {
            MatchSize(inGameRenderer, sprite);
            inGameRenderer.transform.localScale *= scaleBoost;
        }
    }

    private void MatchSize(SpriteRenderer renderer, Sprite newSprite)
    {
        float scaleFactor = renderer.sprite.bounds.size.x / newSprite.bounds.size.x;
        renderer.sprite = newSprite;
        renderer.transform.localScale *= scaleFactor;
    }
}
