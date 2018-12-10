using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTest : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(15);



            //this.gameObject.SetActive(false);
        }
    }
}
