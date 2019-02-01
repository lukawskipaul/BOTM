using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    [SerializeField]
    private float maxHealth = 100.0f;
    
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / maxHealth;
    }
}
