using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for Simple AI movement and attacking based on the position of the player
/// relative to the AI.  Damage and Health are handled in the DamagePlayer and EnemyHealth scripts, respectively.
/// </summary>

public class BasicAI : MonoBehaviour
{
    //Various variables exposed in the Unity Editor for adjustment
    [SerializeField]
    private float playerDistance = 0f,
        lookDistance = 80f,
        chaseDistance = 50f,
        rangedDistance = 20f,
        meleeDistance = 10f,
        movementSpeed = 40f,
        turnSpeed = 1f,
        rangeAttackDelay = 0f,
        meleeAttackDelay = 0f;
    //The transform of the player object, this could be set to a non-Serialized and use FindObjectsWithTag<"Player>
    [SerializeField]
    private Transform player;

    private Rigidbody rb;
    //private Animator anim;
    private Vector3 lookDirection;
    private bool waitActive;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Set the float "speed" in the Animator equal to the magnitude of the AI's velocity
        //anim.SetFloat("speed", rb.velocity.magnitude);
        //Track the Player's distance from the AI
        playerDistance = Vector3.Distance(player.position, transform.position);

        DecideAction();        
    }

    /// <summary>
    /// The DecideAction() method is used to determine what the AI will choose to do based off
    /// of where the player is in regards to preset distances that are editable in the inspector.
    /// Currently, they are nestled if statements, though this could probably be done better using
    /// loops.  This was a quick and dirty way of doing it that worked.  The individual methods are
    /// explained separately where they exist.
    /// </summary>
    private void DecideAction()
    {
        if (playerDistance <= meleeDistance)
        {
            if (!waitActive)
            {
                MeleeAttack();
                StartCoroutine(Wait(meleeAttackDelay));
                AlignToPlayer();
            }
        }
        else if (playerDistance <= rangedDistance)
        {
            if (!waitActive)
            {
                ShootFire();
                StartCoroutine(Wait(rangeAttackDelay));
                AlignToPlayer();
            }
        }
        else if (playerDistance <= chaseDistance)
        {
            if (!waitActive)
            {
                AlignToPlayer();
                MoveTowardsPlayer();
            }
        }
        //The following two else statements reset boolean values in the Animator to false
        else if (playerDistance <= lookDistance)
        {
            //anim.SetBool("chasePlayer", false);
            //anim.SetBool("attackPlayer", false);
            //anim.SetBool("breatheFire", false);
            if (!waitActive)
            {
                AlignToPlayer();
            }
        }
        else
        {
            //anim.SetBool("chasePlayer", false);
            //anim.SetBool("attackPlayer", false);
            //anim.SetBool("breatheFire", false);
        }
    }

    //This method is used to get the AI to rotate towards the player's current position on the y-axis
    private void AlignToPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));

        /* These were attempts to get the AI to turn gradually instead of just instantly looking at the player
         * Root Motion rotation will fix this issue and make it irrelevant
         * transform.rotation = Quaternion.Slerp(transform.rotation, newAlignment, Time.deltaTime);
         * transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed * Time.deltaTime);
        */
    }

    /// <summary>
    /// The methods MoveTowrdsPlayer(), ShootFire(), and MeleeAttack() are mostly just setting bools
    /// in the Animator.  Hitting one turns of the other Animator booleans, while ensuring the intended
    /// Animator boolean is turned on.  MoveTowardsPlayer() will no longer need an AddForce if Root Motion
    /// is used, as it will just move with the Animation.  The DamagePlayer script is placed on the colliders
    /// that will be used in "attacking" the player.  The colliders are enabled and disabled in those
    /// specific Animations using keyframes.  The ShootFire() and MeleeAttack() methods are just ensuring
    /// those Animations will trigger in the Animator.
    /// </summary>
    private void MoveTowardsPlayer()
    {
        //anim.SetBool("attackPlayer", false);
        //anim.SetBool("breatheFire", false);
        //anim.SetBool("chasePlayer", true);
        rb.AddForce(transform.forward * movementSpeed, ForceMode.Acceleration);
    }

    private void ShootFire()
    {       
        //anim.SetBool("chasePlayer", false);
        //anim.SetBool("attackPlayer", false);
        //anim.SetBool("breatheFire", true);
    }
    
    private void MeleeAttack()
    {
        //anim.SetBool("breatheFire", false);
        //anim.SetBool("attackPlayer", true);
    }

    //This method is called through an event on the AI's respective "Death" Animation
    private void RemoveCreature()
    {
        Destroy(gameObject);
    }

    //This method is called through an event at the end of the AI's idle cycle if using multiple idle Animations
    private void ResetIdle()
    {
        //anim.SetTrigger("resetIdle");
    }

    /// <summary>
    /// This will make the AI wait a certain amount of time based on a passed in "delay" value before
    /// executing anything else.  This is primarily used during attacking so that it will finish an
    /// attack animation before doing anything else in the case ofthe player moving into a new and/or
    /// out of an existing attack range.
    /// </summary>
    private IEnumerator Wait(float delay)
    {
        waitActive = true;
        yield return new WaitForSeconds(delay);
        waitActive = false;
    }
}