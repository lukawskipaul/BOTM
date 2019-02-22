using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//prevents this script from being attached to more than one GameObject in a scene
[DisallowMultipleComponent]

//GameObjects with this script require components below, a component will be added if one does not exist
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RootMotionMovementController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;

    private bool canMove;
    private bool canDodge;
    private bool isOnGround;

    private const string baseAttackAnimationName = "Attack Base";
    private const string combo1AttackAnimationName = "Attack Combo 1";

    private void Awake()
    {
        canMove = true;
        canDodge = true;
        isOnGround = true;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (canMove /*&& isOnGround*/)  //TODO: uncomment when walkable surfaces are tagged with "Ground"
        {
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (canMove /*&& isOnGround*/)  //TODO: uncomment when walkable surfaces are tagged with "Ground"
        {
            Move();

            if (canDodge)
            {
                FreeLookDodge();
                LockedOnDodge();
            }
        }
    }

    private void Move()
    {
        //adjusts the float value for "MoveZ" denoted in the attached Animator based on Vertical input
        anim.SetFloat("MoveZ", Input.GetAxis("Vertical"));
        //adjusts the float value for "MoveX" denoted in the attached Animator based on Horizontal input
        anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
    }

    private void Rotate()
    {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            //look with Camera
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
            //lock rotation to only the Y axis
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    private void FreeLookDodge()
    {
        /* Play roll dodge animation when dodge button is pressed and is not locked on */
        if (Input.GetButtonDown("Dodge"))    //checks for lock on in animator
        {
            bool attackAnimationIsPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName(baseAttackAnimationName) || 
                anim.GetCurrentAnimatorStateInfo(0).IsName(combo1AttackAnimationName);

            /* Cancels possible combo attack queuing */
            if (attackAnimationIsPlaying)
            {
                anim.ResetTrigger("Attack");
            }

            anim.SetTrigger("FreeLookDodge");

            //StartCoroutine(DodgeCooldown());

            //rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);     //change to animation event if we need it
        }
    }

    private void LockedOnDodge()
    {
        /* Play hop dodge animation when dodge button is pressed and is locked on */
        if (Input.GetButtonDown("Dodge"))     //checks for lock on in animator
        {
            bool attackAnimationIsPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName(baseAttackAnimationName) ||
                anim.GetCurrentAnimatorStateInfo(0).IsName(combo1AttackAnimationName);

            /* Cancels possible combo attack queuing */
            if (attackAnimationIsPlaying)
            {
                anim.ResetTrigger("Attack");
            }

            anim.SetTrigger("LockedOnDodge");

            //StartCoroutine(DodgeCooldown());

            //rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);     //change to animation event if we need it
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        /* Check if player is on the ground */
        if (other.gameObject.tag == "Ground")       //need to use ground tag for any walkable surface
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        /* Check if player is in mid-air */
        if (other.gameObject.tag == "Ground")
        {
            isOnGround = false;
        }
    }

    private void SetCanMove()
    {
        if (canMove)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    private void OnEnable()
    {
        Telekinesis.TeleManualMovingObject += SetCanMove;
        Telekinesis.TeleStoppedManualMovingObject += SetCanMove;
    }

    private IEnumerator DodgeCooldown()
    {
        canDodge = false;

        yield return new WaitForSecondsRealtime(2.0f);

        canDodge = true;
    }
}
