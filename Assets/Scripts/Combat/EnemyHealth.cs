using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider))]
//This script goes on enemy
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int maxHealth = 100;

    private Animator anim;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healthBar.maxValue = maxHealth;

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void DamageEnemy(int amount)
    {
        /* Damages enemy by player attack amount */
        currentHealth -= amount;

        /* Enemy dies when health reaches 0 */
        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
        }
        else
        {
            anim.SetTrigger("Flinch");
        }
    }

    private void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth;
    }
}
