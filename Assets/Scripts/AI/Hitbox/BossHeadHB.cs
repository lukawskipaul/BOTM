using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player must have a 'Player' tag
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BossHeadHB : MonoBehaviour
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
        parentAnim = this.GetComponentInParent<Animator>();//reference to animator
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
            if (parentAnim.GetBool("isHeadbutting"))
            {
                //Headbutt Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(bossStats.HeadbuttDamage);
                if (showDebug)
                {
                    Debug.Log("HeadButt");
                }
                Collider.enabled = false;
            }
            if (parentAnim.GetBool("isBiting"))
            {
                //Bite Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(bossStats.BiteDamage);
                if (showDebug)
                {
                    Debug.Log("Bite");
                }
                Collider.enabled = false;
            }
        }
    }
}
