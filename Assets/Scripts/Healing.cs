using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField]
    private int healAmount;
    private PlayerHealth thePlayerHealth;

    //private bool kitUsed = false;

    /*
    private void Awake()
    {
        thePlayerHealth = GameObject.FindGameObjectWithTag("Player");
    }
    */

    private void Heal()
    {
        thePlayerHealth.HealPlayer(healAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            thePlayerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (thePlayerHealth.CurrentHealth < thePlayerHealth.MaxHealth)
            {
                Heal();
                Destroy(gameObject);
            }
        }

    }
}
