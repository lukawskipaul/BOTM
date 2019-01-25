using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LastKnownPosition : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<NavMeshAgent>().stoppingDistance = 0f;
        animator.gameObject.GetComponent<NavMeshAgent>().SetDestination(animator.gameObject.GetComponent<EvalEnemy>().GetLastKnowPosition());
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (animator.GetFloat("distanceFromWayPoint") > 0f && animator.GetFloat("distanceFromWayPoint")  < 0.5f)
        {
            animator.SetBool("checkedLastPos", true);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("checkedLastPos", false);
    }
}
