using UnityEngine;
using System.Collections;

public class Dragon : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Shield Settings")]
    public bool shieldActive = false;
    public float shieldDuration = 5f;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Reference")]
    public Transform enemy;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
            Debug.Log("Dragon Health Bar is initialized");
        }
        else
        {
            Debug.Log("Dragon Health Bar is not assigned.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (shieldActive)
        {
            Debug.Log("Shield is active. No damage taken.");
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log($"Dragon took {damage} damage. Current Health: {currentHealth}/{maxHealth}");

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        shieldActive = true;
        Debug.Log("Shield activated!");
        yield return new WaitForSeconds(shieldDuration);
        shieldActive = false;
        Debug.Log("Shield expired.");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log($"Dragon healed {amount}. Current health: {currentHealth}/{maxHealth}");

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    void Die()
    {
        Debug.Log("Game Over! Dragon has been defeated.");
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;
    public bool IsAlive() => currentHealth > 0;
}

