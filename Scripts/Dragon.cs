using UnityEngine;

public class Dragon : MonoBehaviour
{
    [Header("Health Setting")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Reference")]
    public Transform enemy;

    void Start()
    {
        //To initalise Health
        currentHealth = maxHealth;

        //Setting up the health bar
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            Debug.Log("Dragon Health Bar is initialised");
        }
        else
        {
            Debug.Log("Dragon Health Bar is not assigned.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log("Dragon tool {damage} damage. Current Health: {currentHealth}/{maxHealth}");

        //Updating the health bar
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        //Checking if health bar reached 0
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log("Dragon healed {amount}. Current health: {currentHealth}/{maxHealth}");

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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public bool isAlive()
    {
        return currentHealth > 0;
    }
    
}
