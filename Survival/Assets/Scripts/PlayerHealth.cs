using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthBarFill;

    public string gameOverSceneName;
    public float delay = 0.5f;
    public GameObject fadeout;

    //public string nextSceneName; // Name of the next scene to load
    //public float delay = 0.5f; // Delay in seconds before loading the next scene
    //public GameObject fadeout;
    public void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void Damage(float damage)
    {
        Debug.Log($"Player took {damage} damage.");
        currentHealth -= damage;

        // Ensure health never drops below 0
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("health is now: " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        // Ensure health never exceeds maxHealth
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        Debug.Log($"Player healed, now has {currentHealth} HP.");
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    // Function to handle the enemy's death.
    private void Die()
    {
        fadeout.SetActive(true);
        Invoke("LoadGameOverScene", delay);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}
