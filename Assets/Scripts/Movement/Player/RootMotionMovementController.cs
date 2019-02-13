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
    private float dodgeDistance = 1500.0f;

    private Animator anim;
    private Rigidbody rb;

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

    private void FixedUpdate()
    {
        Move();
        //Jump();
        Dodge();
    }

    private void Move()
    {
        //adjusts the float value for "MoveZ" denoted in the attached Animator based on Vertical input
        anim.SetFloat("MoveZ", Input.GetAxis("Vertical"));
        //adjusts the float value for "MoveX" denoted in the attached Animator based on Horizontal input
        anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));

        //adjusts the float value for "Turn" denoted in the attached Animator based on Mouse movement
        //anim.SetFloat("Turn", Input.GetAxis("Mouse X"));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Sets the Animator Trigger for "Jump"
            anim.SetTrigger("Jump");
        }
    }

    private void Dodge()
    {
        if (Input.GetButtonDown("Dodge") /*&& isOnGround*/)
        {
            /* Play dodge animation when dodge button is pressed */
            anim.SetTrigger("Dodge");

            /* Add force in the direction the player is moving and slightly up */
            rb.AddForce(transform.up * 10.0f, ForceMode.Impulse);
            rb.AddForce(rb.velocity.normalized * dodgeDistance, ForceMode.Impulse);
        }
    }
}
