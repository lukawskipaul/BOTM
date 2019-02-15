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
    private int attackDamage = 10;
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
        if (other.tag == playerTag && parentAnim.GetBool("InAttackRange") && !parentAnim.GetBool("isLickingWeapon"))
        {
            if (showDebug)
            {
                Debug.Log("Player Hit!");
            }
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
            parentAnim.SetBool("isLickingWeapon", true);
        }
    }
}
