using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script goes on weapon joint of enemy
public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 10.0f;

    private void OnTriggerEnter(Collider other)
    {
        bool isValidTarget = other.tag == "Player"; //&& enemy is currently attacking

        /* Damages the player if the enemy is currently attacking */
        if (isValidTarget)
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
        }
    }
}
