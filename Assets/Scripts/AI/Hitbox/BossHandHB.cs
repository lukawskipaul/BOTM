using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player must have a 'Player' tag
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BossHandHB : MonoBehaviour
{
    [SerializeField, Tooltip("Claw Attack Damage Output")]
    private int clawDamage = 15;
    [SerializeField, Tooltip("Attack Leap Damage Output")]
    private int attackLeapDamage = 20;
    [SerializeField]
    private bool showDebug = true;
    private Animator parentAnim;//Get animator controller
    public Collider Collider { get; private set; }
    private void Start()
    {
        Collider = this.GetComponent<Collider>();
        Collider.isTrigger = true;//Automatically set collider to a trigger
        Collider.enabled = false;//Initially turns off collider
        parentAnim = this.GetComponentInParent<Animator>();
    }
    /// <summary>
    /// Trigger event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="other">The Object that caused the activation of the trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parentAnim.GetBool("isClawing"))
            {
                //Claw Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(clawDamage);
                if (showDebug)
                {
                    Debug.Log("Claw");
                }
            }
            if (parentAnim.GetBool("isLeapAttacking"))
            {
                ////Attack Leap Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(attackLeapDamage);
                if (showDebug)
                {
                    Debug.Log("Leaping Attack");
                }
            }
        }
    }
}
