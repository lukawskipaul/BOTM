using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Strafe : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;
    NavMeshAgent bossNavMeshAgent;

    GameObject target;  // used for debugging and visualization
                        // comment out in release version of game

    // the maximum distance away from the player
    // that the boss strafes
    float strafeRadius;

    // how fast the boss moves as it strafes
    float strafeSpeed;

    // The position of the boss relative to the player
    // I.E. Player position is the origin of a local graph
    Vector3 bossRelativePosition;

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

        strafeRadius = bossAI.StrafeRadius;
        strafeSpeed = bossAI.StrafeSpeed;

        // The position of the player relative to the Boss
        // I.E. The boss' position is considered local (0,0) and the direction 
        // it's facing decides the orientation of the local x-axis and z-axis
        bossRelativePosition = boss.transform.InverseTransformPoint(player.transform.position);
        Debug.Log("Player.x relative to Boss = " + bossRelativePosition.x);
        Debug.Log("Player.z relative to Boss = " + bossRelativePosition.z);

        // Calculate the closest point between the boss and player along the strafe radius
        target.transform.position = ClosestPoint(boss.transform.position, player.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // for debugging and testing strafing speed only
        {
            strafeSpeed = bossAI.StrafeSpeed;
            player.transform.LookAt(boss.transform);
        }//*/

        // as the boss strafes, it is always turned to face the player
        boss.transform.LookAt(player.transform);

        // if the player is to the boss' left the strafe target moves counterclockwise
        // if the player is to the boss' right the strave target moves clockwise
        if (bossRelativePosition.x >= 0)
        {
            target.transform.RotateAround(player.transform.position, Vector3.up, strafeSpeed * Time.deltaTime);
        }
        else
        {
            target.transform.RotateAround(player.transform.position, Vector3.up, -strafeSpeed * Time.deltaTime);
        }//*/

        // the boss strafes by chasing after the strafing target via the NavMesh
        bossNavMeshAgent.SetDestination(target.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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

    // This function calculates the closest point between the boss 
    // and the player along the strafe radius
    private Vector3 ClosestPoint (Vector3 bossPosition, Vector3 playerPosition)
    {
        float magnitudeBP = MagnitudeBossPlayer(bossPosition, playerPosition);
        float shortX, shortZ;

        shortX = strafeRadius * bossPosition.x / magnitudeBP;
        shortZ = strafeRadius * bossPosition.z / magnitudeBP;

        return new Vector3(shortX, bossPosition.y, shortZ);
    }

    // This function calculates the magnitude of the distance between the boss
    // and player on the XZ plane
    private float MagnitudeBossPlayer (Vector3 bossPosition, Vector3 playerPosition)
    {
        float xDifference = bossPosition.x - playerPosition.x;
        float zDifference = bossPosition.z - playerPosition.z;

        xDifference *= xDifference;
        zDifference *= zDifference;

        return Mathf.Sqrt(xDifference + zDifference);
    }
}
