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
    [SerializeField]
    private float freeLookDodgeDistance = 1500.0f;
    [SerializeField]
    private float lockOnDodgeDistance = 1500.0f;

    private Animator anim;
    private Rigidbody rb;
    private bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canMove)
        {
            Rotate();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            FreeLookDodge();
            //LockOnDodge();
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
        if (Input.GetButtonDown("Dodge") /*&& !isTelekinesing && isOnGround*/)
        {
            /* Play dodge animation when dodge button is pressed */
            anim.SetTrigger("FreeLookDodge");

            /* Add force in the direction the player is moving and slightly up */
            rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            rb.AddForce(rb.velocity.normalized * freeLookDodgeDistance, ForceMode.Impulse);
        }
    }

    private void LockOnDodge()
    {
        if (Input.GetButtonDown("Dodge") /*&& isLockedOn && !isTelekinesing && isOnGround*/)
        {
            /* Play dodge animation when dodge button is pressed */
            anim.SetTrigger("LockOnDodge");

            /* Add force in the direction the player is moving and slightly up */
            rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            rb.AddForce(rb.velocity.normalized * lockOnDodgeDistance, ForceMode.Impulse);
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

    //private void OnEnable()
    //{
    //    Telekinesis.TeleManualMovingObject += SetCanMove;
    //    Telekinesis.TeleStoppedManualMovingObject += SetCanMove;
    //}
}
