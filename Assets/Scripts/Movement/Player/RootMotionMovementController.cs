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
    bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
            Jump();
        }

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
}
