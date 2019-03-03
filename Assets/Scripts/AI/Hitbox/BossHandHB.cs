using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player must have a 'Player' tag
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BossHandHB : MonoBehaviour
{
    [SerializeField]
    private bool showDebug = true;
    private BossEnemyMono bossStats;
    private Animator parentAnim;//Get animator controller
    public Collider Collider { get; private set; }
    private void Start()
    {
        Collider = this.GetComponent<Collider>();
        Collider.isTrigger = true;//Automatically set collider to a trigger
        Collider.enabled = false;//Initially turns off collider
        parentAnim = this.GetComponentInParent<Animator>();//Get reference to animator
        bossStats = this.GetComponentInParent<BossEnemyMono>();
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
                other.GetComponent<PlayerHealth>().DamagePlayer(bossStats.ClawDamage);
                if (showDebug)
                {
                    Debug.Log("Claw");
                }
                Collider.enabled = false;
            }
            if (parentAnim.GetBool("isLeapAttacking"))
            {
                ////Attack Leap Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(bossStats.AttackLeapDamage);
                if (showDebug)
                {
                    Debug.Log("Leaping Attack");
                }
                //Turn off collider when player is hit
                Collider.enabled = false;
            }
        }
    }
}
