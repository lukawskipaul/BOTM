using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attach this script to the enemy hitbox that will deal damage to Player
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class EnemyAttackHitbox : MonoBehaviour
{
    public string playerTag = "Player";
    public bool showDebug = true;
    private void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;//Automatically set collider to a trigger
    }
    /// <summary>
    /// Trigger event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="other">The Object that caused the activation of the trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && this.GetComponentInParent<Animator>().GetBool("InAttackRange"))
        {
            //TODO: Add player losing Health Here
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
            
        }
    }
}
