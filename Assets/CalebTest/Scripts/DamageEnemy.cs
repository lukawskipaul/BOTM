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
    //[SerializeField]
    //private GameObject testEnemy;

    private Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit: " + other.name);

        bool isValidTarget = other.tag == "Enemy" && anim.GetBool("isAttacking") == true;

        /* Damages the enemy if the player is currently attacking */
        if (isValidTarget)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage);
        }
    }

    //void Update()
    //{
    //    //TestAttack();
    //}

    //void TestAttack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        testEnemy.GetComponent<EnemyHealth>().DamageEnemy(attackDamage);
    //    }
    //}
}
