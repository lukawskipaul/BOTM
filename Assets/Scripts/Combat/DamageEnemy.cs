﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

//This script goes on the sword joint of the player
public class DamageEnemy : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private int baseAttackDamage = 25;
    [SerializeField]
    private int combo1AttackDamage = 35;

    private int currentAttackDamage;

    private bool isAttacking;
    public bool IsAttacking
    {
        set
        {
            isAttacking = value;
        }
    }

    #endregion

    private void Awake()
    {
        isAttacking = false;

        currentAttackDamage = baseAttackDamage;
    }

    /* Called in PlayerAttack to change damage to base amount */
    public void ChangeToBaseDamage()
    {
        currentAttackDamage = baseAttackDamage;
    }

    /* Called in PlayerAttack to change damage to combo 1 amount */
    public void ChangeToCombo1Damage()
    {
        currentAttackDamage = combo1AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isValidTarget = other.tag == "Enemy" && isAttacking == true;
        //Debug.Log("Sword triggered");
        /* Damages the enemy if the player is currently attacking */
        if (isValidTarget)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(currentAttackDamage);
            //Debug.Log("Enemy takes damage");
        }
    }
}
