using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    private float rotX;
    private float rotY;
    private float rotZ;
    private float VertRotMin = 0f;
    private float VertRotMax = 65f;
    private float mouseX;
    private float mouseY;
    public bool canMove = true;
    public bool isOnGround = true;
    public bool isSprinting = false;
    public Rigidbody playerRigidbody;
    private Vector3 vector3;

    [SerializeField]
    float distance = 2f;

    [SerializeField]
    GameObject camRig;

    [SerializeField]
    CapsuleCollider playerCollider;

    [SerializeField]
    LayerMask groundLayers;

    [SerializeField]
    public float jumpForce = 100f;

    [SerializeField]
    public float superJumpModifier = 2f;

    [SerializeField]
    float fallModifier = 2f;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        
	}

    void FixedUpdate()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isSprinting = false;
        }
        if (canMove)
        {
            playerRigidbody.detectCollisions = true;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
            {
                isSprinting = true;
                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
            }
            else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = false;
                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey("s"))
            {
                playerRigidbody.transform.position -= playerRigidbody.transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }

            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                playerRigidbody.transform.position += playerRigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }
            else if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                playerRigidbody.transform.position -= playerRigidbody.transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }
            Jump();
            IncreaseFallSpeed();
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            GetComponent<Rigidbody>().AddForce((Vector3.up * jumpForce), ForceMode.Impulse);

            isOnGround = false;
        }

    }

    public void SuperJump()
    {
        if (InputManager.AButton() && Input.GetKey(KeyCode.LeftShift) && isOnGround)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * (jumpForce * superJumpModifier), ForceMode.Impulse);
            isOnGround = false;
        }
    }

    void IncreaseFallSpeed()
    {
        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Physics.gravity * (fallModifier - 1) * Time.deltaTime;
        }
    }

    private void Update()
    {
        IsGrounded();
        //if (canMove)
        //{
        //    playerRigidbody.detectCollisions = true;
        //    rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        //    rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        //    if (rotX < -35f)
        //    {
        //        rotX = -35f;
        //    }
        //    else if (rotX > 35f)
        //    {
        //        rotX = 35f;
        //    }

        //    transform.rotation = Quaternion.Euler(0, rotY, 0);
        //    //GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        //    camRig.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        //}

        playerRigidbody.detectCollisions = true;
        rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        if (rotX < -35f)
        {
            rotX = -35f;
        }
        else if (rotX > 35f)
        {
            rotX = 35f;
        }

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        //GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        camRig.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

    }


    public void IsGrounded()
    {
        //if (Physics.CheckCapsule(playerCollider.bounds.center,
        //    new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.01f, playerCollider.bounds.center.z), playerCollider.radius * 0.9f, groundLayers))
        //{
        //    isOnGround = true;
        //}
        //else
        //{
        //    isOnGround = false;
        //}


        RaycastHit hit;

        Vector3 dir = new Vector3(0, -1);
        Vector3 testDir = new Vector3(0, -distance);
        Debug.DrawRay(transform.position, testDir, Color.red);
        if (Physics.Raycast(transform.position, dir, distance, groundLayers))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

    }

    private void StopMove()
    {
        canMove = false;

    }

    private void StartMove()
    {
        canMove = true;
    }

    private void OnEnable()
    {
        LevitateMoveObject.TeleMovingObject += StopMove;
        LevitateMoveObject.TeleStoppedMovingObject += StartMove;
    }

    private void OnDisable()
    {
        LevitateMoveObject.TeleMovingObject -= StopMove;
        LevitateMoveObject.TeleStoppedMovingObject -= StartMove;
    }
}
