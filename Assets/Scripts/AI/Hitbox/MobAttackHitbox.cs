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
        collider.isTrigger = true;//Automatically set collider to a trigger
        collider.enabled = false;//start with collider turned off <*efficient*>
        parentAnim = this.GetComponentInParent<Animator>();
        crocStats = this.GetComponentInParent<CrocEnemyMono>();
    }
    /// <summary>
    /// Trigger event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="other">The Object that caused the activation of the trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && parentAnim.GetBool("InAttackRange"))
        {
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(crocStats.AttackDamage);
            collider.enabled = false;
            parentAnim.SetTrigger("isLicking");
        }
    }
    
    
}
