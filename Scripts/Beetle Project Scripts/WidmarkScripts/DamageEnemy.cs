using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField]
    private float attackDamage = 15f;

    public AudioSource enemyDamageSound;

    private void Start()
    {
        enemyDamageSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(attackDamage); // player takes damage

            enemyDamageSound.Play();

            //this.gameObject.SetActive(false);
        }
    }

}

