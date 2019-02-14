using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_BackUp : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;
    NavMeshAgent bossNavMeshAgent;

    GameObject target;  // used for debugging and visualization
                        // comment out in release version of game

    Vector3 targetPosition;

    float jumpBackDistance;

    // The position of the boss relative to the player
    // I.E. The player's position is (0,0,0) on a local graph
    Vector3 relativePosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;
        bossNavMeshAgent = bossAI.BossNavMeshAgent;

        target = bossAI.Target; // used for debugging and visualization
                                // deactivate target object in release version of game

        jumpBackDistance = bossAI.JumpBackDistance;

        // rotate boss to face player
        // Placeholder code, need to figure out time-dependant method of turning boss towards objects
        // Slerp???
        boss.transform.LookAt(player.transform);

        relativePosition = boss.transform.InverseTransformPoint(player.transform.position);
        Debug.Log("Player.x relative to Boss = " + relativePosition.x);
        Debug.Log("Player.z relative to Boss = " + relativePosition.z);

        targetPosition = CalculateTargetPoint();
        target.transform.position = targetPosition;

        bossNavMeshAgent.updateRotation = false;
        bossNavMeshAgent.SetDestination(targetPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossNavMeshAgent.updateRotation = true;
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

    // This function calculates the point the boss should move to 
    // when jumping backwards from the player
    Vector3 CalculateTargetPoint()
    {
        Vector3 targetPosition = Vector3.zero;

        Vector3 bossLocal = player.transform.InverseTransformPoint(boss.transform.position);
        Debug.Log("Boss local position = " + bossLocal);

        float bPDistance = Mathf.Sqrt(Mathf.Pow(-bossLocal.x, 2.0f) + Mathf.Pow(-bossLocal.z, 2.0f));
        Debug.Log("Boss local distance from Player = " + bPDistance);

        Debug.Log("Player.x relative to Boss = " + bossLocal.x);
        Debug.Log("Player.z relative to Boss = " + bossLocal.z);

        targetPosition.x = bossLocal.x + (bossLocal.x / bPDistance * jumpBackDistance);
        targetPosition.z = bossLocal.z + (bossLocal.z / bPDistance * jumpBackDistance);

        return player.transform.TransformPoint(targetPosition);
    }//*/
}
