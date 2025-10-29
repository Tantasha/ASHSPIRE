using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SpecialBarSystem : MonoBehaviour
{
    [Header("UI References")]
    public Slider specialBarSlider;
    public GameObject specialGlow;
    public TextMeshProUGUI specialBarText;
    public Button specialButton;
    public ParticleSystem readyParticles;

    [Header("Health System")]
    public Slider healthBar;
    public float maxHealth = 100f;

    [Header("Special Bar Settings")]
    public float maxSpecialEnergy = 100f;
    public float energyGainPerAction = 20f;
    public float healAmount = 50f;
    public float attackBoostAmount = 2f;
    public float boostDuration = 5f;

    [Header("Visual Settings")]
    public float glowPulseSpeed = 1f;
    public Color normalBarColor = Color.yellow;
    public Color healingBarColor = Color.green;
    public Color attackBarColor = Color.red;

    // Private variables
    private float currentSpecialEnergy = 0f;
    private bool isSpecialReady = false;
    private bool isSpecialActive = false;
    private bool isAttackBoosted = false;

    // Public properties
    public bool IsSpecialReady => isSpecialReady;
    public bool IsAttackBoosted => isAttackBoosted;
    public float AttackMultiplier => isAttackBoosted ? attackBoostAmount : 1f;

    void Start()
    {
        currentSpecialEnergy = 0f;
        UpdateSpecialBar();

        if (specialGlow != null)
        {
            specialGlow.SetActive(false);
        }

        if (specialButton != null)
        {
            specialButton.interactable = false;
            specialButton.onClick.AddListener(OnSpecialButtonPressed);
        }

        if (readyParticles != null)
        {
            readyParticles.Stop();
        }

        if (specialBarSlider != null)
        {
            specialBarSlider.value = 0f;
            SetSliderColor(normalBarColor);
        }
    }

    void Update()
    {
        if (isSpecialReady && !isSpecialActive)
        {
            AnimateGlow();
        }
    }

    public void AddSpecialEnergy()
    {
        if (isSpecialActive) return;

        currentSpecialEnergy += energyGainPerAction;

        if (currentSpecialEnergy >= maxSpecialEnergy)
        {
            currentSpecialEnergy = maxSpecialEnergy;
            MakeSpecialReady();
        }

        UpdateSpecialBar();
    }

    void MakeSpecialReady()
    {
        if (isSpecialReady) return;

        isSpecialReady = true;

        if (specialButton != null)
        {
            specialButton.interactable = true;
        }

        if (specialGlow != null)
        {
            specialGlow.SetActive(true);
        }

        if (readyParticles != null)
        {
            readyParticles.Play();
        }

        if (specialBarText != null)
        {
            specialBarText.text = "READY!";
            specialBarText.fontSize = 20;
        }
    }

    void OnSpecialButtonPressed()
    {
        if (!isSpecialReady || isSpecialActive) return;

        float currentHealth = healthBar != null ? healthBar.value : maxHealth;
        float healthPercent = (currentHealth / maxHealth) * 100f;

        if (healthPercent < 50f)
        {
            UseSpecialForHealing();
        }
        else
        {
            UseSpecialForAttackBoost();
        }
    }

    void UseSpecialForHealing()
    {
        isSpecialReady = false;
        isSpecialActive = true;

        if (healthBar != null)
        {
            healthBar.value += healAmount;
            if (healthBar.value > maxHealth)
            {
                healthBar.value = maxHealth;
            }
        }

        SetSliderColor(healingBarColor);
        HideReadyEffects();
        StartCoroutine(DrainSpecialBar());
    }

    void UseSpecialForAttackBoost()
    {
        isSpecialReady = false;
        isSpecialActive = true;
        isAttackBoosted = true;

        SetSliderColor(attackBarColor);
        HideReadyEffects();
        StartCoroutine(DrainSpecialBar());
    }

    IEnumerator DrainSpecialBar()
    {
        float drainRate = maxSpecialEnergy / boostDuration;

        while (currentSpecialEnergy > 0)
        {
            currentSpecialEnergy -= drainRate * Time.deltaTime;
            if (currentSpecialEnergy < 0)
            {
                currentSpecialEnergy = 0;
            }

            UpdateSpecialBar();
            yield return null;
        }

        FinishSpecial();
    }

    void FinishSpecial()
    {
        currentSpecialEnergy = 0f;
        isSpecialActive = false;
        isSpecialReady = false;
        isAttackBoosted = false;

        SetSliderColor(normalBarColor);

        if (specialButton != null)
        {
            specialButton.interactable = false;
        }

        UpdateSpecialBar();
    }

    void UpdateSpecialBar()
    {
        float fillAmount = currentSpecialEnergy / maxSpecialEnergy;

        if (specialBarSlider != null)
        {
            specialBarSlider.value = fillAmount;
        }

        if (specialBarText != null && !isSpecialReady)
        {
            int percent = Mathf.RoundToInt(fillAmount * 100f);
            specialBarText.text = $"SPECIAL: {percent}%";
            specialBarText.fontSize = 16;
        }
        else if (specialBarText != null && isSpecialActive)
        {
            specialBarText.text = "ACTIVE!";
        }
    }

    void SetSliderColor(Color color)
    {
        if (specialBarSlider != null && specialBarSlider.fillRect != null)
        {
            Image fillImage = specialBarSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = color;
            }
        }
    }

    void AnimateGlow()
    {
        if (specialGlow == null) return;

        float pulse = Mathf.PingPong(Time.time * glowPulseSpeed, 1f);

        Image glowImage = specialGlow.GetComponent<Image>();
        if (glowImage != null)
        {
            Color glowColor = glowImage.color;
            glowColor.a = 0.2f + (pulse * 0.5f);
            glowImage.color = glowColor;
        }

        float scale = 1f + (pulse * 0.1f);
        specialGlow.transform.localScale = new Vector3(scale, scale, 1f);
    }

    void HideReadyEffects()
    {
        if (specialGlow != null)
        {
            specialGlow.SetActive(false);
        }

        if (readyParticles != null)
        {
            readyParticles.Stop();
        }
    }

    public float GetCurrentEnergy()
    {
        return currentSpecialEnergy;
    }

    public void SetEnergy(float amount)
    {
        currentSpecialEnergy = Mathf.Clamp(amount, 0f, maxSpecialEnergy);

        if (currentSpecialEnergy >= maxSpecialEnergy)
        {
            MakeSpecialReady();
        }

        UpdateSpecialBar();
    }
}
