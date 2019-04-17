using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attach this script to the enemy hitbox that will deal damage to Player
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class MobAttackHitbox : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private bool showDebug = true;
    private CrocEnemyMono crocStats;
    private Animator parentAnim;
    public Collider collider { get; private set; }
    private void Start()
    {
        collider = this.GetComponent<Collider>();
        collider.isTrigger = false;//Automatically set collider to a trigger
        collider.enabled = false;//start with collider turned off <*efficient*>
        parentAnim = this.GetComponentInParent<Animator>();
        crocStats = this.GetComponentInParent<CrocEnemyMono>();
    }
    /// <summary>
    /// Collision event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="collision">The collider object that caused the activation of the collision event</param>
    private void OnCollisionEnter(Collision collision)
    {
        //Check if its the player & is within attack range
        if (collision.gameObject.tag == playerTag && parentAnim.GetBool("InAttackRange"))
        {
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
            collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(crocStats.AttackDamage);
            collider.enabled = false;
            parentAnim.SetTrigger("isLicking");
        }
    }

}
