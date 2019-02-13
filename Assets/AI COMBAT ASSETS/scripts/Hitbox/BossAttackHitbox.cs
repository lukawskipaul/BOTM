using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private bool showDebug = true;
    private Animator parentAnim;
    private void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;//Automatically set collider to a trigger
        parentAnim = this.GetComponentInParent<Animator>();
    }
    /// <summary>
    /// Trigger event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="other">The Object that caused the activation of the trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && parentAnim.GetBool("IsAttacking"))
        {
            //TODO: Add player losing Health Here
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
        }
    }
}
