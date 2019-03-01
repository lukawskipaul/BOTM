using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_WalkForwardClose : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;
    NavMeshAgent bossNavMeshAgent;
    Vector3 lookpos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;
        bossNavMeshAgent = bossAI.BossNavMeshAgent;
        bossNavMeshAgent.SetDestination(player.transform.position);
        //Select random attack action
        animator.SetInteger("AttackChoice", (int)(Random.value * 8));
        bossNavMeshAgent.isStopped = false;
        //Debug.Log(animator.GetInteger("AttackChoice"));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetFloat("distanceFromPlayerSq") >  40 && animator.GetFloat("distanceFromPlayerSq") < 500)
        {
            bossNavMeshAgent.destination = player.transform.position;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossNavMeshAgent.isStopped = true;
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
