using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private float maxHealth = 100f;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void DamagePlayer(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //TODO: die
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
