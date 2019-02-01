using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

//This script goes on player
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDelay = 1.0f;

    private Animator anim;

    private bool canAttack;
    private bool waitActive;

    private void Start()
    {
        canAttack = true;
        waitActive = false;
        
        anim = this.gameObject.GetComponent<Animator>();
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
            canAttack = false;

            anim.SetTrigger("Attack");
            anim.SetBool("isAttacking", true);  //TODO: use a better way to check if is attacking
            StartCoroutine(Wait(attackDelay));

            canAttack = true;
        }
    }

    private IEnumerator Wait(float delay)
    {
        /* Wait until end of attack animation to be able to attack again */
        waitActive = true;

        yield return new WaitForSecondsRealtime(delay); //TODO: change to end of animation

        anim.SetBool("isAttacking", false);
        waitActive = false;
    }
}
