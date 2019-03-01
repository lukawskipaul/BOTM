using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AttackBite : StateMachineBehaviour
{
    Vector3 lookpos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Activates Head Hitbox
        animator.gameObject.GetComponentInChildren<BossHeadHB>().Collider.enabled = true;
        animator.SetBool("isBiting", true);

        //Set look position to player
        //lookpos = animator.GetComponent<BossEnemyMono>().Player.transform.position - animator.transform.position;
        //animator.gameObject.transform.rotation = Quaternion.LookRotation(lookpos, Vector3.up);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set boss rotation to look at player
        //animator.gameObject.transform.rotation = Quaternion.LookRotation(lookpos,Vector3.up);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Deactivates Head Hitbox
        animator.gameObject.GetComponentInChildren<BossHeadHB>().Collider.enabled = false;
        animator.SetBool("isBiting", false);
        //animator.gameObject.transform.rotation = Quaternion.LookRotation(lookpos, Vector3.up);
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
