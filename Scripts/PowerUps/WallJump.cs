using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    private Vector3 moveVector;
    private Vector3 lastMove;
    private float speed = 8;
    private float jumpforce = 8;
    private float gravity = 25;
    private float verticalVelocity;
    private CharacterController controller;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveVector = Vector3.zero;
        moveVector.x = InputManager.MainHorizontal();
        moveVector.z = InputManager.MainVertical();
        if(controller.isGrounded)
        {
            verticalVelocity = -1;
            if(InputManager.AButton())
            {
                verticalVelocity = jumpforce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }

        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= speed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;
	}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!controller.isGrounded && hit.normal.y <0.1f)
        {
            if(InputManager.AButton())
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
                verticalVelocity = jumpforce;
                moveVector = hit.normal * speed;
            }
        }
    }
}
