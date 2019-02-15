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

    private void Awake()
    {
        canAttack = true;
    }

    private void Start()
    {
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
        if (Input.GetButtonDown("Attack") && canAttack)
        {
            anim.SetTrigger("Attack");
        }
    }

    /* Called at start of attack animation to prevent being able to attack again */
    public void StartAttackAnimation()
    {
        canAttack = false;
    }

    /* Called at end of attack animation to allow being able to attack again */
    public void EndAttackAnimation()
    {
        canAttack = true;
    }

    /* Called during specific animation frame to start doing damage to hit enemies */
    public void StartDamageWindow()
    {
        swordAttack.IsAttacking = true;
        Time.timeScale = 0.25f;
    }

    /* Called during specific animation frame to stop doing damage to hit enemies */
    public void EndDamageWindow()
    {
        swordAttack.IsAttacking = false;
        Time.timeScale = 1.0f;
    }

    private void SetCanAttack()
    {
        if (canAttack)
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
    }

    private void OnEnable()
    {
        Telekinesis.TeleManualMovingObject += SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject += SetCanAttack;
    }

    private void OnDisable()
    {
        Telekinesis.TeleManualMovingObject -= SetCanAttack;
        Telekinesis.TeleStoppedManualMovingObject -= SetCanAttack;
    }
}
