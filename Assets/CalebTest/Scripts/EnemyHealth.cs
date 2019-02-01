using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script goes on enemy
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
        /* Damages enemy by player attack amount */
        currentHealth -= amount;

        /* Enemy dies when health reaches 0 */
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth / maxHealth;
    }
}
