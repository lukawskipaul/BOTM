using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//This script goes on player
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int maxHealth = 100;

    private PlayerRespawnScript respawn;

    private int currentHealth;
    private bool isInvulnerable;

    public static event Action TakeDamage;

    private void Awake()
    {
        currentHealth = maxHealth;
        isInvulnerable = false;
    }

    private void Start()
    {
        respawn = this.gameObject.GetComponent<PlayerRespawnScript>();

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void DamagePlayer(int amount)
    {
        /* Damages player by enemy attack amount if not during iframe */
        if (!isInvulnerable)
        {
            currentHealth -= amount;

            OnTakeDamage();
        }

        /* Player dies when health reaches 0 */
        if (currentHealth <= 0)
        {
            respawn.RespawnPlayer();
        }
    }

    private void OnTakeDamage()
    {
        /* Invokes TakeDamage event */
        if (TakeDamage != null)
        {
            TakeDamage.Invoke();
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

    private void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth / maxHealth;
    }

    /* Called at specific dodge animation frame to make player invulnerable */
    public void MakeInvulnerable()
    {
        isInvulnerable = true;
    }

    /* Called at specific dodge animation frame to make player vulnerable */
    public void MakeVulnerable()
    {
        isInvulnerable = false;
    }
}
