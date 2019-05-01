using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Patrol : StateMachineBehaviour
{
    private AIPatrol patrol;
    private NavMeshAgent navMeshAgent;

    private float originalSpeed;
    private float originalStoppingDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = animator.gameObject.GetComponent<AIPatrol>();
        navMeshAgent = animator.gameObject.GetComponent<NavMeshAgent>();

        patrol.IsPatrolling = true;
        originalSpeed = navMeshAgent.speed;
        originalStoppingDistance = navMeshAgent.stoppingDistance;
        navMeshAgent.speed = patrol.PatrolSpeed;
        navMeshAgent.stoppingDistance = patrol.PatrolStoppingDistance;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent.speed = originalSpeed;
        navMeshAgent.stoppingDistance = originalStoppingDistance;
        patrol.IsPatrolling = false;
        navMeshAgent.isStopped = true;//Stops the AI from moving
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
