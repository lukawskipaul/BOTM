using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePlayer : MonoBehaviour
{
    

    
    [SerializeField]
    private float attackDamage = 15f;

    

    void Start()
    {
      
    }

    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("You have taken damage");
            other.gameObject.GetComponent<PlayerHealthTest>().DamagePlayer(attackDamage); // player takes damage
        }
    }
}
