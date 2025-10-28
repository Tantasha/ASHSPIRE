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
    public float energyGainPerAction = 20f;  // Gain per attack/action
    public float healAmount = 50f;           // HP restored when used
    public float attackBoostAmount = 2f;     // Damage multiplier
    public float boostDuration = 5f;         // How long boost lasts

    [Header("Visual Settings")]
    public float glowPulseSpeed = 2f;
    public Color normalBarColor = Color.yellow;
    public Color healingBarColor = Color.orange;
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
        // Initialize
        currentSpecialEnergy = 0f;
        UpdateSpecialBar();

        // Make sure glow is off
        if (specialGlow != null)
        {
            specialGlow.SetActive(false);
        }

        // Disable button initially
        if (specialButton != null)
        {
            specialButton.interactable = false;
            specialButton.onClick.AddListener(OnSpecialButtonPressed);
        }

        // Stop particles initially
        if (readyParticles != null)
        {
            readyParticles.Stop();
        }

        if(specialBarSlider != null)
        {
            specialBarSlider.value = 0f;
            SetSliderColor(normalBarColor);
        }
    }

    void Update()
    {
        // Animate glow when ready
        if (isSpecialReady && !isSpecialActive)
        {
            AnimateGlow();
        }

    }

    // Call this method when player performs an action (attack, etc.)
    public void AddSpecialEnergy()
    {
        if (isSpecialActive) return;  // Don't gain energy while active

        currentSpecialEnergy += energyGainPerAction;

        Debug.Log($"Special energy gained! Current: {currentSpecialEnergy}/{maxSpecialEnergy}");

        // Check if full
        if (currentSpecialEnergy >= maxSpecialEnergy)
        {
            currentSpecialEnergy = maxSpecialEnergy;
            MakeSpecialReady();
        }

        UpdateSpecialBar();
    }

    void MakeSpecialReady()
    {
        if (isSpecialReady) return;  // Already ready

        isSpecialReady = true;

        Debug.Log("SPECIAL READY!");

        // Enable button
        if (specialButton != null)
        {
            specialButton.interactable = true;
        }

        // Show glow
        if (specialGlow != null)
        {
            specialGlow.SetActive(true);
        }

        // Play particles
        if (readyParticles != null)
        {
            readyParticles.Play();
        }

        // Update text
        if (specialBarText != null)
        {
            specialBarText.text = "READY!";
            specialBarText.fontSize = 20;
        }
    }

    void OnSpecialButtonPressed()
    {
        if (!isSpecialReady || isSpecialActive) return;

        // Check if health is low
        float currentHealth = healthBar != null ? healthBar.value : maxHealth;
        float healthPercent = (currentHealth / maxHealth) * 100f;

        if (healthPercent < 50f)  // If health below 50%
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
        Debug.Log("SPECIAL USED: HEALING!");

        isSpecialReady = false;
        isSpecialActive = true;

        // IMMEDIATELY HEAL
        if (healthBar != null)
        {
            healthBar.value += healAmount;

            // Clamp to max
            if (healthBar.value > maxHealth)
            {
                healthBar.value = maxHealth;
            }

            Debug.Log($"Healed {healAmount} HP! Current HP: {healthBar.value}");
        }

        // Change bar color to green
        SetSliderColor(healingBarColor);
        // Hide glow and particles
        HideReadyEffects();

        // Drain the bar
        StartCoroutine(DrainSpecialBar());
    }

    void UseSpecialForAttackBoost()
    {
        Debug.Log("SPECIAL USED: ATTACK BOOST!");

        isSpecialReady = false;
        isSpecialActive = true;
        isAttackBoosted = true;

        // Change bar color to red
        SetSliderColor(attackBarColor);


        // Hide glow and particles
        HideReadyEffects();

        // Drain the bar
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

        // Special finished
        FinishSpecial();
    }

    void FinishSpecial()
    {
        Debug.Log("Special ability finished!");

        currentSpecialEnergy = 0f;
        isSpecialActive = false;
        isSpecialReady = false;
        isAttackBoosted = false;

        // Reset bar color
        SetSliderColor(normalBarColor);

        // Disable button
        if (specialButton != null)
        {
            specialButton.interactable = false;
        }

        UpdateSpecialBar();
    }

    void UpdateSpecialBar()
    {
        //if (specialBarFill == null) return;

        // Update fill amount
        float fillAmount = currentSpecialEnergy / maxSpecialEnergy;
        if (specialBarSlider != null)
        {
            specialBarSlider.value = fillAmount;
        }



        // Update text
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
        if(specialBarSlider != null && specialBarSlider.fillRect != null)
        {
            Image fillImage = specialBarSlider.fillRect.GetComponent<Image>();
            if(fillImage != null)
            {
                fillImage.color = color;
            }
        }
    }

    void AnimateGlow()
    {
        if (specialGlow == null) return;

        // Pulse effect
        float pulse = Mathf.PingPong(Time.time * glowPulseSpeed, 1f);

        Image glowImage = specialGlow.GetComponent<Image>();
        if (glowImage != null)
        {
            Color glowColor = glowImage.color;
            glowColor.a = 0.2f + (pulse * 0.5f);  // Pulse between 0.2 and 0.7
            glowImage.color = glowColor;
        }

        // Scale pulse
        float scale = 1f + (pulse * 0.1f);
        specialGlow.transform.localScale = new Vector3(scale, scale, 1f);
    }

    void HideReadyEffects()
    {
        // Hide glow
        if (specialGlow != null)
        {
            specialGlow.SetActive(false);
        }

        // Stop particles
        if (readyParticles != null)
        {
            readyParticles.Stop();
        }
    }

    // Public method to check current energy (for debugging)
    public float GetCurrentEnergy()
    {
        return currentSpecialEnergy;
    }

    // Public method to manually set energy (for testing)
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