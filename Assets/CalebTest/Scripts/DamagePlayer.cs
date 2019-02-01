using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 10.0f;

    void OnTriggerEnter(Collider other)
    {
        bool isValidTarget = other.tag == "Player"; //&& enemy is currently attacking

        if (isValidTarget)
        {
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(attackDamage);
        }
    }
}
