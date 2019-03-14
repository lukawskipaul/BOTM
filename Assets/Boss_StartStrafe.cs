using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_StartStrafe : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;

    // The position of the player relative to the boss
    Vector3 relativePosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;

        // The position of the player relative to the Boss
        // I.E. The boss' position is considered local (0,0) and the direction 
        // it's facing decides the orientation of the local x-axis and z-axis
        relativePosition = boss.transform.InverseTransformPoint(player.transform.position);
        // If the player is to the right of the boss, the boss should strafe clockwise
        if (relativePosition.x >= 0)
        {
            animator.Play("Strafe_Left");
        }
        // If the player is to the left of the boss, the boss should strafe counterclockwise
        else
        {
            animator.Play("Strafe_Right");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

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
