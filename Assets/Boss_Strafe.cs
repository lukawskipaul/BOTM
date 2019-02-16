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

    float strafeRadius;
    float strafeSpeed;

    Vector3 relativeX;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set Animator Parameter 'isStrafing' to true
        //*Needed for head rotation
        animator.SetBool("isStrafing",true);

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
        relativeX = boss.transform.InverseTransformPoint(player.transform.position);
        Debug.Log("Player.x relative to Boss = " + relativeX.x);
        Debug.Log("Player.z relative to Boss = " + relativeX.z);

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

        boss.transform.LookAt(player.transform);

        if (relativeX.x >= 0)
        {
            target.transform.RotateAround(player.transform.position, Vector3.up, strafeSpeed * Time.deltaTime);
        }
        else
        {
            target.transform.RotateAround(player.transform.position, Vector3.up, -strafeSpeed * Time.deltaTime);
        }//*/

        bossNavMeshAgent.SetDestination(target.transform.position);

       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Set Animator Parameter 'isStrafing' to false
        //*Needed for head rotation
        animator.SetBool("isStrafing",false);
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

    private Vector3 ClosestPoint (Vector3 bossPosition, Vector3 playerPosition)
    {
        float magnintudeBP = MagnitudeBossPlayer(bossPosition, playerPosition);
        float shortX, shortZ;

        Vector3 playerLocal = player.transform.InverseTransformPoint(playerPosition);
        Debug.Log("Player local position = " + playerLocal);
        Vector3 bossLocal = player.transform.InverseTransformPoint(bossPosition);
        Debug.Log("Boss local position = " + bossLocal);

        shortX = playerLocal.x + (strafeRadius * (bossLocal.x - playerLocal.x) / magnintudeBP);
        shortZ = playerLocal.z + (strafeRadius * (bossLocal.z - playerLocal.z) / magnintudeBP);

        //target.transform.position.y;
        return player.transform.TransformPoint(new Vector3(shortX, 0, shortZ));
    }

    private float MagnitudeBossPlayer (Vector3 bossPosition, Vector3 playerPosition)
    {
        float xDifference = bossPosition.x - playerPosition.x;
        float zDifference = bossPosition.z - playerPosition.z;

        xDifference *= xDifference;
        zDifference *= zDifference;

        return Mathf.Sqrt(xDifference + zDifference);
    }
}
