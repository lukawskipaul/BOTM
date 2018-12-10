using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [SerializeField]
    float attackDelay = 0f;

    //Temp Variables
    [SerializeField]
    private BoxCollider swordCollider;
    [SerializeField]
    private GameObject swordVFX;
    //End of Temp Variables

    Animator anim;

    AudioSource[] audioSources;
    AudioSource swordSwing;
    AudioSource swordWhoosh;

    bool canAttack;
    bool waitActive;

    private void Start()
    {
        canAttack = true;
        waitActive = false;

        audioSources = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();

        swordSwing = audioSources[2];
        swordWhoosh = audioSources[3];

        //Temp
        swordVFX.gameObject.SetActive(false);
    }

    private void Update()
    {
        Attack();
    }


    private void Attack()   //should not be in movement script
    {
        if ((Input.GetButtonDown("Melee Attack") || Input.GetAxis("Melee Attack") > 0.0f) && canAttack)
        {
            int attackNumber;
            System.Random random = new System.Random();
            attackNumber = random.Next(1, 100);

            canAttack = false;
            anim.SetTrigger("Attack");
            anim.SetInteger("AttackNumber", attackNumber);

            swordSwing.volume = Random.Range(0.9f, 1.1f);
            swordSwing.pitch = Random.Range(0.85f, 1.1f);
            swordSwing.Play();

            swordWhoosh.volume = Random.Range(0.9f, 1.1f);
            swordWhoosh.pitch = Random.Range(0.85f, 1.1f);
            swordWhoosh.Play();
            StartCoroutine(Wait(attackDelay));
        }
    }

    //Hopefully Temp Code, called in Animation
    private void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }

    private void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

    private void EnableVFX()
    {
        swordVFX.gameObject.SetActive(true);
    }

    private void DisableVFX()
    {
        swordVFX.gameObject.SetActive(false);
    }
    //End of Temp Code

    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
        canAttack = true;
    }
}
