
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Health Setting")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Attack Effects")]
    public ParticleSystem attackEffect;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Reference")]
    public Transform dragon;
    private Dragon dragonScript;

    [Header("Attack Setting")]
    public int minDamage = 5;
    public int maxDamage = 10;
    public float minAttackDelay = 3f; //Minimum seconds between attacks
    public float maxAttackDelay = 5f; //Maximum seconds between attacks
    public bool autoAttack = true; //Toggle for auto attack 
    public bool isAttacking = false;

    [Header("Scene Transition")]
    public string loadScene = "controls";
    public float delayBeforeTransition = 0.5f;


    void Start()
    {
        //Initialise Health
        currentHealth = maxHealth;
        //Setting up health bar
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            Debug.Log("Enemy health bar initialised");
        }
        else
        {
            Debug.Log("Enemy health bar has yet to be assigned.");
        }

        //Getting the Dragon script reference
        if (dragon != null)
        {
            dragonScript = dragon.GetComponent<Dragon>();
            if (dragonScript == null)
            {
                Debug.Log("Enemy: Dragon script is not found.");
            }
        }
        else
        {
            Debug.Log("Enemy: Dragon reference is not assigned.");
        }

        //start auto-attacking
        if (autoAttack)
        {
            StartCoroutine(AutoAttack());
        }
    }

    //Coroutine Auto Attacking
    IEnumerator AutoAttack()
    {
        //Wait before the first attack
        yield return new WaitForSeconds(Random.Range(minAttackDelay, maxAttackDelay));

        while (IsAlive() && dragonScript != null && dragonScript.IsAlive())
        {
            //Attacking the dragon
            AttackDragon();

            float waitTime = Random.Range(minAttackDelay, maxAttackDelay);
            Debug.Log("Enemy will attack again in {waitTime:F1} seconds");
            yield return new WaitForSeconds(waitTime);
        }
        Debug.Log("Enemy stopped attacking.");
    }

    //Attack the dragon
    public void AttackDragon()
    {
        if (dragonScript == null || !dragonScript.IsAlive())
        {
            Debug.Log("Enemy cannot attack. Dragon is dead.");
            return;
        }
        if (!IsAlive())
        {
            Debug.Log("Enemy cannot attack. Dragon is dead.");
            return;
        }

        //Play the attack effect particle
        if (attackEffect != null) 
        {
            attackEffect.Play();
            Debug.Log("Enemy attack effect played.");
        }
        
        //To calculate random damages
        int damage = Random.Range(minDamage, maxDamage + 1);
        Debug.Log("Enemy attacks the Dragon for {damage} damages.");

        //Does damage to dragon
        dragonScript.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Enemy took {damage} damage. Current health: {currentHealth}/{maxHealth}");

        //updating health bar
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        //checking if the enemy die
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Victory! Enemy has been defeated.");
        StopAllCoroutines(); //Stop attacking
        gameObject.SetActive(false);

        StartCoroutine(TransitionToGameScene());
    }

    IEnumerator TransitionToGameScene()
    {
        //short delay when return to the game scene
        yield return new WaitForSeconds(delayBeforeTransition);

        //Check if scene exists
        if(Application.CanStreamedLevelBeLoaded(loadScene))
        {
            SceneManager.LoadScene(loadScene);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    
    public void SetAutoAttack()
    {
        autoAttack = enabled;
        if(enabled && !isAttacking)
        {
            StartCoroutine(AutoAttack());
        } else if (!enabled)
        {
            StopAllCoroutines();
        }
    }
}

