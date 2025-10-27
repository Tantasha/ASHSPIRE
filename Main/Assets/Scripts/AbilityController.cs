using UnityEngine.UI;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [Header("Particle System Reference")]
    public ParticleSystem attackParticle;
    public ParticleSystem shieldParticle;
    public ParticleSystem healParticle;

    [Header("Button Reference")]
    public Button attackBtn;
    public Button shieldBtn;
    public Button healBtn;

    [Header("Cooldown overlay")]
    public Image attackOverlay;
    public Image shieldOverlay;
    public Image healOverlay;


    [Header("Cooldown setting")]

    public float attackCD = 5f;
    public float shieldCD = 10f;
    public float healCD = 15f;

    [Header("CD Timer")]
    private float attackTimer = 0f;
    private float shieldTimer = 0f;
    private float healTimer = 0f;

    void Start()
    {
        //Linking the button to the methods
        attackBtn.onClick.AddListener(OnAttackBtnClick);
        shieldBtn.onClick.AddListener(OnShieldBtnClick);
        healBtn.onClick.AddListener(OnHealBtnClick);

        //Hiding the overlay
        attackOverlay.fillAmount = 0;
        shieldOverlay.fillAmount = 0;
        healOverlay.fillAmount = 0;
    }

    void Update()
    {
        //Counting timer 
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            attackOverlay.fillAmount = attackTimer / attackCD;
        } else
        {
            attackOverlay.fillAmount = 0;
        }

        if (shieldTimer > 0)
        {
            shieldTimer -= Time.deltaTime;
            shieldOverlay.fillAmount = shieldTimer / shieldCD;
        } else
        {
            shieldOverlay.fillAmount = 0;
        }

        if (healTimer > 0)
        {
            healTimer -= Time.deltaTime;
            healOverlay.fillAmount = healTimer / healCD;
        } else
        {
            healOverlay.fillAmount = 0;
        }

        //Update the button states
        attackBtn.interactable = (attackTimer <= 0);
        shieldBtn.interactable = (shieldTimer <= 0);
        healBtn.interactable = (healTimer <= 0);
    }

    void OnAttackBtnClick()
    {
        //checking if cooldown is ready
        if (attackTimer > 0) return;

        Debug.Log("Attack is activated.");
        attackParticle.Play();

        //Start the cooldown
        attackTimer = attackCD;

    }

    void OnShieldBtnClick()
    {
        if (shieldTimer > 0) return;

        Debug.Log("Shield is activated.");
        shieldParticle.Play();

        shieldTimer = shieldCD;
    }

    void OnHealBtnClick() {
        if (healTimer > 0) return;

        Debug.Log("Heal is activated.");
        healParticle.Play();

        healTimer = healCD;
    }
}
