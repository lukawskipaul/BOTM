using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_AttackLeap : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Activate Hitbox On Boss' Hands
        animator.gameObject.GetComponentInChildren<BossHandHB>().Collider.enabled = true;
        animator.SetBool("isLeapAttacking", true);
        //Select a random evasive action
        animator.SetInteger("EvasiveChoice",(int)Random.value * 4);//0-3
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Deactivate Hitbox On Boss' Hands
        if (animator.gameObject.GetComponentInChildren<BossHandHB>().Collider.enabled)
        {
            animator.gameObject.GetComponentInChildren<BossHandHB>().Collider.enabled = false;
        }
        animator.SetBool("isLeapAttacking",false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
