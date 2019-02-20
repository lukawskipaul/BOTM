using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_JumpBack : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;
    NavMeshAgent bossNavMeshAgent;

    // the maximum distance the boss moves
    // as it jumps backwards
    float jumpBackDistance;

    // the position the boss moves towards
    // as it jumps back
    Vector3 jumpBackPosition;

    float lookRotationSpeed;

    LayerMask obstacleMask;

    Quaternion lookAtPlayer;

    GameObject target;  // used for debugging and visualization
                        // comment out in release version of game

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
        jumpBackDistance = bossAI.JumpBackDistance;
        lookRotationSpeed = bossAI.LookRotationSpeed;
        obstacleMask = bossAI.ObstacleMask;

        // used for debugging
        // comment out on release version of game
        
        //  -   -   -
        target = bossAI.Target;

        bossRelativePosition = boss.transform.InverseTransformPoint(player.transform.position);
        Debug.Log("Player.x relative to Boss = " + bossRelativePosition.x);
        Debug.Log("Player.z relative to Boss = " + bossRelativePosition.z);
        //  -   -   -
        //*/

        // make sure that the boss is facing the player
        lookAtPlayer = Quaternion.LookRotation(player.transform.position - boss.transform.position);

        // calculate jump back target position
        jumpBackPosition = JumpBackTarget(boss.transform.position, player.transform.position, jumpBackDistance);
        target.transform.position.Set(jumpBackPosition.x, target.transform.position.y, jumpBackPosition.z);

        RaycastHit hitInfo;
        if (Physics.Linecast(boss.transform.position, jumpBackPosition, out hitInfo, obstacleMask))
        {
            Debug.Log("Path blocked");
            Debug.Log(hitInfo.distance);
            Debug.Log(hitInfo.point);
            target.transform.position.Set(hitInfo.point.x, target.transform.position.y, hitInfo.point.z);
        }
        else
        {
            Debug.Log("Path free");
        }

        // check if target is in unreachable location
        // Navmesh should be able to handle this,
        // but leave comment here in case

        // set boss destination
        //bossNavMeshAgent.updatePosition = false;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Quaternion.Dot(boss.transform.rotation, lookAtPlayer) < 0.99f)
        {
            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, lookAtPlayer, lookRotationSpeed * Time.deltaTime);
        }
        else
        {
            bossNavMeshAgent.updateRotation = false;

            bossNavMeshAgent.SetDestination(jumpBackPosition);
        }

        //Debug.Log(Quaternion.Dot(boss.transform.rotation, lookAtPlayer));
        //Debug.Log(bossNavMeshAgent.updatePosition);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // reenable boss rotation
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

    // This function calculates the target position the boss jumps back
    private Vector3 JumpBackTarget(Vector3 bossPosition, Vector3 playerPosition, float jumpBackDistance)
    {
        float magnitudeBP = MagnitudeBossPlayer(bossPosition, playerPosition);
        float targetX, targetZ;

        targetX = bossPosition.x + ((bossPosition.x - playerPosition.x) / magnitudeBP * jumpBackDistance);
        targetZ = bossPosition.z + ((bossPosition.z - playerPosition.z) / magnitudeBP * jumpBackDistance);

        /*
        Debug.Log("JumpBackDistance = " + jumpBackDistance);
        Debug.Log("MagnitudeBP = " + magnitudeBP);
        Debug.Log("Boss.x = " + bossPosition.x);
        Debug.Log("Boss.z = " + bossPosition.z);
        Debug.Log("Target.x = " + targetX);
        Debug.Log("Target.z = " + targetZ);
        //*/

        return new Vector3(targetX, bossPosition.y, targetZ);
    }

    // This function calculates the magnitude of the distance between the boss
    // and player on the XZ plane
    private float MagnitudeBossPlayer(Vector3 bossPosition, Vector3 playerPosition)
    {
        float xDifference = bossPosition.x - playerPosition.x;
        float zDifference = bossPosition.z - playerPosition.z;

        xDifference *= xDifference;
        zDifference *= zDifference;

        return Mathf.Sqrt(xDifference + zDifference);
    }
}
