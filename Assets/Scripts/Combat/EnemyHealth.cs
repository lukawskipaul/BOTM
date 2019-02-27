using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(CapsuleCollider))]

//This script goes on the enemies
public class EnemyHealth : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int maxHealth = 100;

    private Animator anim;

    private int currentHealth;

    #endregion

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();

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
            AkSoundEngine.PostEvent("Play_CrocDeath1", gameObject);
        }
        else
        {
            anim.SetTrigger("Flinch");
            AkSoundEngine.PostEvent("Play_CrocFlinch1", gameObject);
        }
    }

    private void UpdateHealthBar()
    {
        /* Updates health bar with current health */
        healthBar.value = currentHealth;
    }
}
