using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float strafeSpeed = 20.0f;
    [SerializeField]
    float walkSpeed = 35.0f;
    [SerializeField]
    float runSpeed = 100.0f;
    [SerializeField]
    float turnSpeed = 5.0f;
    [SerializeField]
    float jumpStrength = 500.0f;
    [SerializeField]
    float dodgeDistance = 2500.0f;

    [SerializeField]
    PhysicMaterial zeroFriction;
    [SerializeField]
    PhysicMaterial maxFriction;
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    AudioClip[] footstepsArray;

    Rigidbody rb;
    Animator anim;
    CapsuleCollider cc;
    
    AudioSource[] audioSources;
    AudioSource footstep;

    Vector3 facingDirection;
    Vector3 previousDirection;

    float horizontalInput;
    float verticalInput;
    float moveSpeed;

    bool isRunning;
    bool isJumping;
    bool isAiming;
    bool isOnGround;
    bool isTakingStep;

    const string horizontalAxisName = "Horizontal";
    const string verticalAxisName = "Vertical";
    const string sprintButtonName = "Sprint";
    const string jumpButtonName = "Jump";
    const string aimButtonName = "Aim";

    void Awake()
    {
        isRunning = false;
        isJumping = false;
        isAiming = false;
        isOnGround = true;
        isTakingStep = false;
    }

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>();
        cc = this.gameObject.GetComponentInChildren<CapsuleCollider>();
        audioSources = this.gameObject.GetComponents<AudioSource>();

        footstep = audioSources[0];
        

        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ChangeFrictionMaterial();

    }

    void FixedUpdate()
    {
        Dodge();
        MovePlayer();
        PlayFootstep();
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetFloat("Speed", rb.velocity.x + rb.velocity.z);
    }

    void ChangeFrictionMaterial()
    {
        if (horizontalInput == 0.0f && verticalInput == 0.0f)
        {
            cc.material = maxFriction;      //a lot of friction when wanting to stop so player doesn't slide forever
        }
        else
        {
            cc.material = zeroFriction;     //zero friction when wanting to move so player actually moves
        }
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        verticalInput = Input.GetAxis(verticalAxisName);
        isJumping = Input.GetButtonDown(jumpButtonName);

        if (Input.GetButton(aimButtonName) || Input.GetAxis(aimButtonName) > 0.0f)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
        
        previousDirection = cameraTransform.right;

        if (isOnGround)
        {
            //movement controls
            if (Input.GetButton(sprintButtonName) || Input.GetAxis(sprintButtonName) > 0.0f)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (isRunning && !isAiming)
            {
                moveSpeed = runSpeed;
            }
            else if (!isAiming)
            {
                moveSpeed = walkSpeed;
            }
            else
            {
                moveSpeed = strafeSpeed;
            }

            rb.AddForce(((previousDirection * horizontalInput) + (cameraTransform.forward * verticalInput)) * moveSpeed / Time.deltaTime);

            //jump controls
            if (isJumping && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

                //TODO: play jump sound
            }
        }
        
        //rotation controls
        if (horizontalInput != 0.0f && !isAiming || verticalInput != 0.0f && !isAiming)     //only rotate player when moving and not aiming
        {
            facingDirection = transform.position + (previousDirection * horizontalInput) + (cameraTransform.forward * verticalInput);
            Vector3 dir = facingDirection - this.gameObject.transform.position;
            dir.y = 0;  //stops player from tipping over

            float angle = Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(dir));

            if (angle != 0.0f)
            {
                rb.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);       //rotate player to face look direction
            }
        }

        if (isAiming)
        {
            Vector3 dir = cameraTransform.forward;
            dir.y = 0;  //stops player from tipping over

            float angle = Quaternion.Angle(this.gameObject.transform.rotation, Quaternion.LookRotation(dir));

            if (angle != 0.0f)
            {
                rb.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);       //rotate player to face aim direction
            }
        }
    }

    void PlayFootstep()
    {
        //TODO: add check for ground type
        //TODO: change footstep array to specific type (i.e. gravelArray)

        if (rb.velocity.magnitude > 2.0f && !footstep.isPlaying && !isTakingStep && isOnGround)
        {
            //choose random clip, excluding the first one
            int n = Random.Range(1, footstepsArray.Length - 1);
            footstep.clip = footstepsArray[n];

            //randomizes volume and pitch for every step, increasing with walk speed
            footstep.volume = Mathf.Clamp(Random.Range(0.45f, 0.65f) * rb.velocity.magnitude, 0.0f, 1.0f);
            footstep.pitch = Random.Range(0.85f, 1.15f) * (Mathf.Clamp(rb.velocity.magnitude / 7, 1.0f, 1.5f));

            footstep.Play();

            //move clip to first index so it won't play again right away
            footstepsArray[n] = footstepsArray[0];
            footstepsArray[0] = footstep.clip;

            StartCoroutine(TakeStep());
        }
    }

    void Dodge()
    {
        if (Input.GetButtonDown("Dodge") && isOnGround)
        {
            anim.SetTrigger("Dodge");
            rb.AddForce(transform.forward * dodgeDistance, ForceMode.Impulse);
        }

        //isDodging = true;

        //Implement dodge timer
        //Implement I-frames counter
        //Add Dodge Anim
    }

    IEnumerator TakeStep()
    {
        isTakingStep = true;

        //pause in between playing footstep sound, decreasing with walk speed
        yield return new WaitForSecondsRealtime(0.5f / (Mathf.Clamp(rb.velocity.magnitude / 7.0f, 0.8f, 1.5f)));

        isTakingStep = false;
    }

    void OnCollisionEnter(Collision other)
    {
        //check if on the ground
        if (other.gameObject.tag == "Ground")       //need to use ground tag for any walkable surface
        {
            isOnGround = true;
            rb.drag = 5.0f;        //increase drag when on the ground
        }
    }
    
    void OnCollisionExit(Collision other)
    {
        //check if left the ground
        if (other.gameObject.tag == "Ground")
        {
            isOnGround = false;
            rb.drag = 0.0f;        //decrease drag when in the air
        }
    }    
}