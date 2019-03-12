using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float inputDelay = 0.1f;// input DeadZone
    public float forwardVelocity = 12;
    public float rotateVelocity = 100;

    Quaternion targetRotation;
    Rigidbody rb;
    float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
        else
            Debug.LogError("Character does not have a Rigidbody.");

        forwardInput = turnInput = 0;
    }
    void GetInput()
    {
        forwardInput = Input.GetAxis("Veritcal");
        turnInput = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        
    }

    void Walk()
    {
        if (Mathf.Abs(forwardInput) > inputDelay)
        {
            //move
            rb.velocity = transform.forward * forwardInput * forwardVelocity;
        }
        else
            //zero velocity if less then deadzone
            rb.velocity = Vector3.zero;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
            transform.rotation = targetRotation;
    }
}
