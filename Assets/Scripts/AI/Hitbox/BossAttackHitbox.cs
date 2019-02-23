using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{

    [SerializeField]
    private string playerTag = "Player";
    [SerializeField]
    private bool showDebug = true;
    private Animator parentAnim;
    public Collider collider { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider>();
        collider.isTrigger = true;//Automatically set collider to a trigger
        collider.enabled = false;//start with collider turned off <*efficient*>
        parentAnim = this.GetComponentInParent<Animator>();
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
            collider.enabled = false;
        }
    }
}
