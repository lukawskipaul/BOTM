using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

//This script goes on player
public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private DamageEnemy swordAttack;

    private bool canAttack;

    private void Start()
    {
        canAttack = true;
        
        anim = this.gameObject.GetComponent<Animator>();
        swordAttack = this.gameObject.GetComponentInChildren<DamageEnemy>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        /* Play attack animation when attack button is pressed */
        if (Input.GetButtonDown("Attack") && canAttack /*&& !isTelekinesing*/)
        {
            anim.SetTrigger("Attack");
        }
    }

    /* Called at start of attack animation to prevent being able to attack again */
    public void StartAttackEvent()
    {
        canAttack = false;
    }

    /* Called at end of attack animation to allow being able to attack again */
    public void EndAttackEvent()
    {
        canAttack = true;
    }

    /* Called during specific animation frame to start doing damage to hit enemies */
    public void StartDamageEvent()
    {
        swordAttack.IsAttacking = true;
        Time.timeScale = 0.2f;
    }

    /* Called during specific animation frame to stop doing damage to hit enemies */
    public void EndDamageEvent()
    {
        swordAttack.IsAttacking = false;
        Time.timeScale = 1;
    }
}
