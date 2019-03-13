using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [System.Serializable]
    public class MoveSettings
    {
        public float forwardVelocity = 12;
        public float rotateVelocity = 100;
        public float distToGrounded = 0.1f;
        public LayerMask ground;

    }
    [System.Serializable]
    public class PhysicsSettings
    {

    }
    [System.Serializable]
    public class InputSettings
    {
        public float inputDelay = 0.1f;// input DeadZone
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
    }

    public MoveSettings movesetting = new MoveSettings();
    public PhysicsSettings physicsetting = new PhysicsSettings();
    public InputSettings inputsetting = new InputSettings();

    Vector3 velocity = Vector3.zero;
    Quaternion targetRotation;
    Rigidbody rb;
    float forwardInput, turnInput;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, movesetting.distToGrounded, movesetting.ground);
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
        forwardInput = Input.GetAxis(inputsetting.FORWARD_AXIS);
        turnInput = Input.GetAxis(inputsetting.TURN_AXIS);
    }

    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Walk();
        rb.velocity = transform.TransformDirection(velocity);
    }

    void Walk()
    {
        if (Mathf.Abs(forwardInput) > inputsetting.inputDelay)
        {
            //move
            //rb.velocity = transform.forward * forwardInput * movesetting.forwardVelocity;
            velocity.z = movesetting.forwardVelocity * forwardInput;
        }
        else
            //zero velocity if less then deadzone
            //rb.velocity = Vector3.zero;
            velocity.z = 0;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputsetting.inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(movesetting.rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
            transform.rotation = targetRotation;
    }
}
