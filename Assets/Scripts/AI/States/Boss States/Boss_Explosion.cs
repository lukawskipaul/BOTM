using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Explosion : StateMachineBehaviour
{
    GameObject boss;
    GameObject player;
    BossAI bossAI;

    // The current radius of the explosion
    float currentExplosionRadius;

    // The maximum radius of the explosion
    float maximumExplosionRadius;

    // The rate that the explosion grows over time
    float explosionRateOfGrowth;

    // The scalar that affects the radius of the explosion
    [Range(0.0f, 1.0f)]
    float explosionScalar;

    // The list of objects that the explosion will attempt to hit
    LayerMask targetMask;

    // The list of objects that intercept the explosion
    LayerMask obstacleMask;

    // Bool checking if explosion is occuring
    bool isExploding;

    // The list of objects that are within range of the explosion
    List<Transform> openTargets;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // establish variables
        boss = animator.gameObject;
        bossAI = boss.GetComponent<BossAI>();
        player = bossAI.Player;

        currentExplosionRadius = 0.0f;
        maximumExplosionRadius = bossAI.MaximumExplosionRadius;
        explosionRateOfGrowth = bossAI.ExplosionRateOfGrowth;
        explosionScalar = 0.0f;

        targetMask = bossAI.TargetMask;
        obstacleMask = bossAI.ObstacleMask;

        isExploding = true;

        openTargets = new List<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Explode();

        // For debugging and visualization
        {
            explosionRateOfGrowth = bossAI.ExplosionRateOfGrowth;
            bossAI.CurrentExplosionRadius = this.currentExplosionRadius;
            bossAI.OpenTargets = this.openTargets;
            //isExploding = true;

            Debug.Log(isExploding);
            Debug.Log(explosionScalar);
        }//*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the target list for the next time the boss enters this state
        openTargets.Clear();

        // For debugging and visualization
        {
            bossAI.CurrentExplosionRadius = 0.0f;
            bossAI.OpenTargets.Clear();
        }//*/
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

    void Explode()
    {
        // As long as the explosion remains below the maximum radius
        // It continues to grow in size
        if (isExploding && currentExplosionRadius < maximumExplosionRadius)
        {
            // Explosion grows independently of framerate, instead scaling
            // at fixed intervals on the assumpetion that the game is constantly at 60 FPS
            explosionScalar += (explosionRateOfGrowth * Time.deltaTime * 60.0f);
            if (explosionScalar > 1.0f)
            {
                explosionScalar = 1.0f;
            }
            Debug.Log("Exploding");
        }
        // When the explosion goes above the maximum radius
        // Its size is reset to zero and the explosion is set to false
        else
        {
            isExploding = false;
            explosionScalar = 0.0f;
            Debug.Log("Reseting");
        }

        // The current size of the explosion is determined as
        // the product of the maximum radius by the explosion scalar
        //
        // For this reason, the explosion scalar must always be between 0 and 1
        currentExplosionRadius = maximumExplosionRadius * explosionScalar;

        // Check if there are any open targets 
        // within range of the current explosion radius
        CheckInRange();
    }

    // This function checks if there are any unguarded targets
    // within the range of the current explosion radius
    void CheckInRange()
    {
        // reset the target list every time
        // this function is called
        openTargets.Clear();

        // Find all objects within range of the explosion radius
        // tagged as a target
        Collider[] targetsInRange = Physics.OverlapSphere(boss.transform.position, currentExplosionRadius, targetMask);

        // For each target in range,
        // check if there is an obstacle in between
        // the explosion and the target
        for (int i = 0; i < targetsInRange.Length; ++i)
        {
            Transform target = targetsInRange[i].transform;
            Vector3 directionToTarget = (target.transform.position - boss.transform.position).normalized;
            float distanceToTarget = Vector3.Distance(boss.transform.position, target.position);

            // A raycast checks if there as an obstacle blocking the explosion
            // from the target, and if not, the target is added to the list of open targets
            if (!Physics.Raycast(boss.transform.position, directionToTarget, distanceToTarget, obstacleMask))
            {
                openTargets.Add(target);
            }
        }
    }
}
