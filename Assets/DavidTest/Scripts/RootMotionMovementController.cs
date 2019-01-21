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

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        //adjusts the float value for "MoveZ" denoted in the attached Animator
        anim.SetFloat("MoveZ", Input.GetAxis("Vertical"));
        //adjusts the float value for "MoveX" denoted in the attached Animator
        anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Sets the Animator Trigger for "Jump"
            anim.SetTrigger("Jump");
        }
    }
}
