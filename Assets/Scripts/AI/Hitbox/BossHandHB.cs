﻿using System.Collections;
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
        Collider.isTrigger = false;//Automatically set collider to a collision collider
        Collider.enabled = true;//Initially turns on collider
        parentAnim = this.GetComponentInParent<Animator>();//Get reference to animator
        bossStats = this.GetComponentInParent<BossEnemyMono>();
    }
    /// <summary>
    /// Collision event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="collision">The collider object that caused the activation of the collision event</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (parentAnim.GetBool("isClawing"))
            {
                //Claw Damage output towards Player
                collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(bossStats.ClawDamage);
                if (showDebug)
                {
                    Debug.Log("Claw");
                }
            }
            if (parentAnim.GetBool("isLeapAttacking"))
            {
                ////Attack Leap Damage output towards Player
                collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(bossStats.AttackLeapDamage);
                if (showDebug)
                {
                    Debug.Log("Leaping Attack");
                }
            }
        }
    }
}
