using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script goes on the enemies
public class EnemyHealth : MonoBehaviour
{
    public static event Action EnemyDied;
    #region Variables

    [SerializeField]
    private GameObject healthBarObject;
    [SerializeField]
    private int maxHealth = 100;

    private Animator anim;
    private Slider healthBar;

    private int currentHealth;
    [HideInInspector]
    public bool isDead = false;

    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        healthBar = healthBarObject.GetComponent<Slider>();
        healthBar.maxValue = maxHealth;

        UpdateHealthBar();
    }

    private void Update()
    {
        if (!isDead)
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
            isDead = true;
            healthBar.value = 0;
            Destroy(healthBarObject);
            Debug.Log("Enemy died here");
            this.gameObject.tag = "DeadEnemy";
            OnEnemyDied();
        }
        else
        {
            //anim.SetTrigger("Stun");
            //AkSoundEngine.PostEvent("Play_BodySquish", gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth;
    }

    private void OnEnemyDied()
    {
        if (EnemyDied != null)
        {
            EnemyDied.Invoke();
        }
    }
}
