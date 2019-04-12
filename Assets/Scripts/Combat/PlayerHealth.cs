using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(Animator))]

//This script goes on the player
public class PlayerHealth : MonoBehaviour
{
    #region Variables
    
    [SerializeField]
    private float regenCooldownInSeconds = 5.0f;
    [SerializeField]
    private float healthRegenSpeed = 5.0f;

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

    private float currentHealth;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    private bool isInvulnerable;
    private bool canRegen;

    public static event Action TakeDamage;

    private const string takeDamageAnimationTriggerName = "TakeDamage";
    private const string attackAnimationBooleanName = "Attack";
    private const string tkPullAnimationTriggerName = "TKPull";
    private const string freeLookDodgeAnimationTriggerName = "FreeLookDodge";
    private const string lockedOnDodgeAnimationTriggerName = "LockedOnDodge";

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
    }

    private void Update()
    {
        CapHealth();

        //HealthRegen();
    }

    public void DamagePlayer(int amount)
    {
        /* Damages player by enemy attack amount if not during iframe */
        if (!isInvulnerable)
        {
            /* Cancels possible combo attack queuing */
            anim.SetBool(attackAnimationBooleanName, false);

            /* Cancels possible tk pull queuing */
            anim.ResetTrigger(tkPullAnimationTriggerName);

            /* Cancels possible dodge queuing */
            anim.ResetTrigger(freeLookDodgeAnimationTriggerName);
            anim.ResetTrigger(lockedOnDodgeAnimationTriggerName);

            /* Plays take damage animation */
            anim.SetTrigger(takeDamageAnimationTriggerName);

            currentHealth -= amount;

            StopCoroutine(DisableHealthRegen());
            StartCoroutine(DisableHealthRegen());

            OnTakeDamage();
        }

        /* Player dies when health reaches 0 */
        if (currentHealth <= 0)
        {
            respawn.RespawnPlayer();
        }
    }

    public void HealPlayer(int amount)
    {
        /* Heals player by pickup amount */
        currentHealth += amount;
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
