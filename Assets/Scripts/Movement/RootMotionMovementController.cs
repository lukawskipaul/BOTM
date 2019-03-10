using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prevents this script from being attached to more than one GameObject in a scene
[DisallowMultipleComponent]

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

//This script goes on the player
public class RootMotionMovementController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float dodgeCooldownInSeconds = 2.0f;

    private Animator anim;
    private Rigidbody rb;

    private bool canMove;
    private bool canDodge;
    private bool isOnGround;
    public bool IsOnGround
    {
        set
        {
            isOnGround = value;
        }
    }

    private const string dodgeButtonName = "Dodge";
    private const string baseAttackBooleanName = "isAttackBase";
    private const string combo1AttackBooleanName = "isAttackCombo";
    private const string attackAnimationBooleanName = "Attack";
    private const string tkPullAnimationTriggerName = "TKPull";
    private const string freeLookDodgeAnimationTriggerName = "FreeLookDodge";
    private const string lockedOnDodgeAnimationTriggerName = "LockedOnDodge";

    #endregion

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
        if (canMove)
            //&& isOnGround)
        {
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            //&& isOnGround)
        {
            Move();

            if (canDodge)
            {
                FreeLookDodge();
                LockedOnDodge();
            }
            else
            {
                CancelQueuingDuringDodgeCooldown();
            }
        }
        else
        {
            Debug.Log("Player not on Ground");
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
        if (Input.GetButtonDown(dodgeButtonName))    //checks for lock on in animator
        {
            /* Cancels possible combo attack queuing */
            anim.SetBool(attackAnimationBooleanName, false);

            /* Cancels possible tk pull queuing */
            anim.ResetTrigger(tkPullAnimationTriggerName);

            anim.SetTrigger(freeLookDodgeAnimationTriggerName);

            //rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);     //change to animation event if we need it
        }
    }

    private void LockedOnDodge()
    {
        /* Play hop dodge animation when dodge button is pressed and is locked on */
        if (Input.GetButtonDown(dodgeButtonName))     //checks for lock on in animator
        {
            /* Cancels possible combo attack queuing */
            anim.SetBool(attackAnimationBooleanName, false);

            /* Cancels possible tk pull queuing */
            anim.ResetTrigger(tkPullAnimationTriggerName);

            anim.SetTrigger(lockedOnDodgeAnimationTriggerName);

            //rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);     //change to animation event if we need it
        }
    }

    private void CancelQueuingDuringDodgeCooldown()
    {
        /* Cancel attack queuing even when dodge cooldown is active */
        if (Input.GetButtonDown(dodgeButtonName))
        {
            /* Cancels possible combo attack queuing */
            anim.SetBool(attackAnimationBooleanName, false);

            /* Cancels possible tk pull queuing */
            anim.ResetTrigger(tkPullAnimationTriggerName);
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

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

    /* Called at specific dodge animation frame to start dodge cooldown */
    public void StartDodgeCooldown()
    {
        StopCoroutine(DodgeCooldown());
        StartCoroutine(DodgeCooldown());
    }

    /* Starts cooldown for the player's dodge ability */
    private IEnumerator DodgeCooldown()
    {
        canDodge = false;

        yield return new WaitForSecondsRealtime(dodgeCooldownInSeconds);

        canDodge = true;
    }

    #endregion
}
