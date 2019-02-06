using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

//This script goes on sword joint of player
public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 25.0f;

    private bool isAttacking;
    public bool IsAttacking
    {
        set
        {
            isAttacking = value;
        }
    }

    private void Start()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isValidTarget = other.tag == "Enemy" && isAttacking == true;

        /* Damages the enemy if the player is currently attacking */
        if (isValidTarget)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage);
        }
    }
}
