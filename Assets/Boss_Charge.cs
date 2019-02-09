using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Charge : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    GameObject target;
    BossAI bossAI;
    NavMeshAgent bossNavMeshAgent;

    Vector3 targetPosition;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;
        target = bossAI.Target;
        bossNavMeshAgent = bossAI.BossNavMeshAgent;

        // rotate boss to face player
        boss.transform.LookAt(player.transform);

        // calculate vector distance between boss and player
        targetPosition = player.transform.position - boss.transform.position;

        // increase distance by charge distance scalar
        targetPosition *= bossAI.ChargeDistanceScalar;

        // set boss destination
        targetPosition += boss.transform.position;
        target.transform.position = targetPosition;
        bossNavMeshAgent.SetDestination(targetPosition);

        // make boss faster
        bossNavMeshAgent.speed *= bossAI.SpeedScalar;
        bossNavMeshAgent.acceleration *= bossAI.AccelerationScalar;
        bossNavMeshAgent.angularSpeed *= bossAI.AngularSpeedScalar;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reset boss speed
        bossNavMeshAgent.speed /= bossAI.SpeedScalar;
        bossNavMeshAgent.acceleration /= bossAI.AccelerationScalar;
        bossNavMeshAgent.angularSpeed /= bossAI.AngularSpeedScalar;
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
