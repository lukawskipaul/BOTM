﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//This script goes on the player
public class PlayerHealth : MonoBehaviour
{
    #region Variables
    
    [SerializeField]
    private float regenCooldownInSeconds = 5.0f;
    [SerializeField]   private float healthRegenSpeed = 5.0f;
    
    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    private Animator anim;
    private PlayerRespawnScript respawn;
    private DamageEnemy damageEnemy;

    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
        }
    }

    private bool isInvulnerable;
    private bool canRegen;

    public static event Action TakeDamage;

    private const string takeDamageTriggerName = "TakeDamage";
    private const string deathBoolName = "isDying";

    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
        isInvulnerable = false;
        canRegen = true;
    }

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        respawn = this.gameObject.GetComponent<PlayerRespawnScript>();
        damageEnemy = GetComponentInChildren<DamageEnemy>();
    }

    /*
    private void Update()
    {
        CapHealth();

        //TODO: HealthRegen();
    }
    */

    public void DamagePlayer(int amount)
    {
        /* Damages player by enemy attack amount if not during iframe */
        if (!isInvulnerable)
        {
            currentHealth -= amount;

            StopCoroutine(DisableHealthRegen());
            StartCoroutine(DisableHealthRegen());

            /* Death animation plays when health reaches 0, otherwise getting hit animation plays */
            if (currentHealth <= 0)
            {
                anim.SetBool(deathBoolName, true);
                damageEnemy.IsAttacking = false;
            }
            else
            {
                anim.SetTrigger(takeDamageTriggerName);
                damageEnemy.IsAttacking = false;
            }

            OnTakeDamage();
        }
    }

    public void HealPlayer(int amount)
    {
        /* Heals player by pickup amount */
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void CapHealth()
    {
        /* Caps player health at 100% */
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;

            canRegen = false;
        }
    }

    void HealthRegen()
    {
        /* Regenerates health over time if the player hasn't been damaged in a while */
        if (canRegen)
        {
            currentHealth += Time.deltaTime * healthRegenSpeed;
        }
    }

    private IEnumerator DisableHealthRegen()
    {
        /* Stops health regen when the player takes damage */
        canRegen = false;

        yield return new WaitForSecondsRealtime(regenCooldownInSeconds);

        canRegen = true;
    }

    private void OnTakeDamage()
    {
        /* Invokes TakeDamage event */
        if (TakeDamage != null)
        {
            TakeDamage.Invoke();
        }
    }

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

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

    /* Called at specific death animation frame to make player respawn */
    public void Respawn()
    {
        respawn.RespawnPlayer();
    }

    #endregion
}
