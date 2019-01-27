using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attach this script to the enemy hitbox that will deal damage to Player
/// </summary>
public class EnemyAttackHitbox : MonoBehaviour
{
    public string playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag && this.GetComponent<Animator>().GetBool("isAttacking"))
        {
            //TODO: Add player losing Health Here
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
