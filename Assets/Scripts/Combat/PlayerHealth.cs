using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script goes on player
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void DamagePlayer(int amount)
    {
        /* Damages player by enemy attack amount */
        currentHealth -= amount;

        /* Player dies when health reaches 0 */
        if (currentHealth <= 0)
        {
            GetComponent<PlayerRespawnScript>().RespawnPlayer();
        }
    }

    public void HealPlayer(int amount)
    {
        /* Heals player by pickup amount */
        currentHealth += amount;

        /* Caps player health at 100% */
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth / maxHealth;
    }
}
