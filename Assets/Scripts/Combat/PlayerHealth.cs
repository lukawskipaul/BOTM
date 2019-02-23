using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//This script goes on player
public class PlayerHealth : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int maxHealth = 100;

<<<<<<< HEAD
    private PlayerRespawnScript respawn;
=======
    private Animator anim;
>>>>>>> Mechanics

    private int currentHealth;

<<<<<<< HEAD
    public static event Action TakeDamage;

    #endregion

    private void Awake()
=======
    void Start()
>>>>>>> Mechanics
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        anim = GetComponent<Animator>();

<<<<<<< HEAD
    private void Start()
    {
        respawn = this.gameObject.GetComponent<PlayerRespawnScript>();

=======
>>>>>>> Mechanics
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void DamagePlayer(int amount)
    {
<<<<<<< HEAD
        /* Damages player by enemy attack amount if not during iframe */
        if (!isInvulnerable)
        {
            currentHealth -= amount;

            OnTakeDamage();
        }
=======
        /* Damages player by enemy attack amount */
        currentHealth -= amount;
>>>>>>> Mechanics

        /* Player dies when health reaches 0 */
        if (currentHealth <= 0)
        {
<<<<<<< HEAD
            respawn.RespawnPlayer();
        }
    }

    private void OnTakeDamage()
    {
        /* Invokes TakeDamage event */
        if (TakeDamage != null)
        {
            TakeDamage.Invoke();
=======
            //anim.SetTrigger("Die");
            GetComponent<PlayerRespawnScript>().RespawnPlayer();
>>>>>>> Mechanics
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
<<<<<<< HEAD
        healthBar.value = currentHealth / maxHealth;
    }

    #region Animation Events

    /* Called at specific dodge animation frame to make player invulnerable */
    public void MakeInvulnerable()
    {
        isInvulnerable = true;
    }

    /* Called at specific dodge animation frame to make player vulnerable */
    public void MakeVulnerable()
    {
        isInvulnerable = false;
=======
        healthBar.value = currentHealth;
>>>>>>> Mechanics
    }

    #endregion
}
