using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attach this script to the enemy hitbox that will deal damage to Player
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class MobAttackHitbox : MonoBehaviour
{
    //Serialize Components
    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private float attackDamage = 10.0f;
    [SerializeField]
    private bool showDebug = true;

    private Animator parentAnim;
    private Collider collider;

    private void Start()
    {
        collider = this.GetComponent<Collider>();//Automatically set collider to a trigger
        collider.isTrigger = true;
        parentAnim = this.GetComponentInParent<Animator>();
    }
    /// <summary>
    /// Trigger event which detects whether the hitbox collided with the player
    /// </summary>
    /// <param name="other">The Object that caused the activation of the trigger event</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && parentAnim.GetBool("InAttackRange") && !parentAnim.GetBool("isLickingWeapon"))
        {
            //TODO: Add player losing Health Here
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
            parentAnim.SetBool("isLickingWeapon",true);
            collider.enabled = false;
        }
    }
    //Encapsulate a reference to the collider this script is attach to(Enemy hit box)
    public Collider GetCollider()
    {
        return collider;
    }
}
