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
                        // deactivate in release version of game

    // The maximum distance that the boss should be from
    // the player as it strafes
    float strafeRadius;

    // The speed at which the target circles the player
    // If the boss' strafe isn't smooth, increase this
    float strafeSpeed;

    // How fast the boss turns to look at the player
    float lookRotationSpeed;

    // The rotation angle between the boss and the player
    // the boss needs to turn so that it is facing the player
    Quaternion lookAtPlayer;

    // Is the boss finished turning towards the player
    bool lookingAtPlayer;

    // The position of the player relative to the boss
    Vector3 relativePosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;
        bossNavMeshAgent = bossAI.BossNavMeshAgent;
        lookRotationSpeed = bossAI.LookRotationSpeed;
        target = bossAI.Target;

        strafeRadius = bossAI.StrafeRadius;
        strafeSpeed = bossAI.StrafeSpeed;

        // Makes sure that the boss will always turn towards player before strafing
        lookingAtPlayer = false;

        // Calculate rotation angle between boss and player
        lookAtPlayer = Quaternion.LookRotation(player.transform.position - boss.transform.position);

        // The position of the player relative to the Boss
        // I.E. The boss' position is considered local (0,0) and the direction 
        // it's facing decides the orientation of the local x-axis and z-axis
        relativePosition = boss.transform.InverseTransformPoint(player.transform.position);

        {
            /*
            //Debug.Log("Player.x relative to Boss = " + relativeX.x);
            //Debug.Log("Player.z relative to Boss = " + relativeX.z);
            if (relativePosition.x >= 0)
            {
                Debug.Log("Player.x >= 0, boss should move clockwise");
            }
            else
            {
                Debug.Log("Player.x < 0, boss should move counterclockwise");
            }
            //*/
        }

        // Find the closest point between the boss and player on the strafe radius
        // And place the target on it
        target.transform.position = ClosestPoint(boss.transform.position, player.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // for debugging and testing strafing speed only
        // comment out when proper strafing speed is found
        strafeSpeed = bossAI.StrafeSpeed;

        // The boss rotates to face the player before strafing
        if (Quaternion.Dot(boss.transform.rotation, lookAtPlayer) < 0.90f && !lookingAtPlayer)
        {
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, lookAtPlayer, lookRotationSpeed * Time.deltaTime);
        }    
        // As the boss strafes, it always turns to face the player
        else
        {
            // if lookingAtPlayer is true, the boss is facing the player
            // and can begin strafing
            lookingAtPlayer = true;

            // The boss is always looking at the player as it moves 
            boss.transform.LookAt(player.transform);

            // If the player is to the right of the boss, the boss should strafe clockwise
            if (relativePosition.x >= 0)
            {
                target.transform.RotateAround(new Vector3(player.transform.position.x, 5.0f, player.transform.position.z), Vector3.up, strafeSpeed * Time.deltaTime);

            }
            // If the player is to the left of the boss, the boss should strafe counterclockwise
            else
            {
                target.transform.RotateAround(new Vector3(player.transform.position.x, 5.0f, player.transform.position.z), Vector3.up, -strafeSpeed * Time.deltaTime);
            }
        }//*/

        // Raises the height of the target above ground
        // Used for testing
        target.transform.position = new Vector3(target.transform.position.x, 2.0f, target.transform.position.z);

        // The boss chases after the target
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

    // This function calculates the point along the strafe radius
    // that is closest to the boss
    private Vector3 ClosestPoint (Vector3 bossPosition, Vector3 playerPosition)
    {
        // the magnitude of between the boss and player on the XZ-plane
        float magnintudeBP = MagnitudeBossPlayer(bossPosition, playerPosition);

        float shortX, shortZ;

        shortX = playerPosition.x + (strafeRadius * (bossPosition.x - playerPosition.x) / magnintudeBP);
        shortZ = playerPosition.z + (strafeRadius * (bossPosition.z - playerPosition.z) / magnintudeBP);

        return new Vector3(shortX, 2.0f, shortZ);
    }

    // This function calculates the magnitude between the boss
    // and player on the XZ plane
    // Expensive program, so use sparingly
    private float MagnitudeBossPlayer (Vector3 bossPosition, Vector3 playerPosition)
    {
        float xDifference = bossPosition.x - playerPosition.x;
        float zDifference = bossPosition.z - playerPosition.z;

        xDifference *= xDifference;
        zDifference *= zDifference;

        return Mathf.Sqrt(xDifference + zDifference);
    }
}
