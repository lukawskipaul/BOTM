using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackDelay = 1.0f;

    Animator anim;

    private bool canAttack;
    private bool waitActive;

    private void Start()
    {
        canAttack = true;
        waitActive = false;
        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Attack") && canAttack)
        {
            canAttack = false;

            anim.SetTrigger("Attack");
            anim.SetBool("isAttacking", true);  //use a better way to check if is attacking
            StartCoroutine(Wait(attackDelay));  //change to end of animation

            canAttack = true;
        }
    }

    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSecondsRealtime(delay);
        anim.SetBool("isAttacking", false);
        waitActive = false;
    }
}
