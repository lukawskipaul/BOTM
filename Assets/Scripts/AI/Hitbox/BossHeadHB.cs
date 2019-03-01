using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Player must have a 'Player' tag
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class BossHeadHB : MonoBehaviour
{
    [SerializeField, Tooltip("Headbutt Damage Output")]
    private int headbuttDamage = 10;
    [SerializeField, Tooltip("Bite Damage Output")]
    private int biteDamage = 25;
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
            if (parentAnim.GetBool("isHeadbutting"))
            {
                //Headbutt Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(headbuttDamage);
                if (showDebug)
                {
                    Debug.Log("HeadButt");
                }
            }
            if (parentAnim.GetBool("isBiting"))
            {
                //Bite Damage output towards Player
                other.GetComponent<PlayerHealth>().DamagePlayer(biteDamage);
                if (showDebug)
                {
                    Debug.Log("Bite");
                }
            }
        }
    }
}
