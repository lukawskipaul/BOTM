using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class cameraController : MonoBehaviour
{
    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    [SerializeField]
    float zOffset;

    [SerializeField]
    float normalFOV = 60.0f;
    [SerializeField]
    float aimingFOV = 45.0f;

    [SerializeField]
    float mouseSensitivity = 5.0f;
    [SerializeField]
    Transform target;

    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .05f;
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    private float yaw;
    private float pitch;

    bool lockCursor;
    bool isAiming;

    const string horizontalAxisName = "Mouse X"; //was "Camera Horizontal"
    const string verticalAxisName = "Mouse Y";  //was "Camera Vertical"
    const string aimButtonName = "Aim";

    void Awake()
    {
        lockCursor = true;
        isAiming = false;
    }

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        HandleAiming();
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void HandleAiming()
    {
        if (Input.GetButton(aimButtonName) || Input.GetAxis(aimButtonName) > 0.0f)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        if (isAiming)
        {
            Camera.main.fieldOfView = aimingFOV;

            //TODO: make crosshair appear
            //TODO: change to ranged attack
        }
        else
        {
            Camera.main.fieldOfView = normalFOV;

            //TODO: make crosshair disappear
            //TODO: change to melee attack
        }
    }

    void FollowPlayer()
    {
        yaw += Input.GetAxis(horizontalAxisName) * mouseSensitivity;
        pitch -= Input.GetAxis(verticalAxisName) * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        //Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = currentRotation;
        
        if (isAiming)
        {
            this.gameObject.transform.position = target.position - (transform.forward * zOffset / 3) + (transform.up * yOffset) + (transform.right * xOffset);
        }
        else
        {
            this.gameObject.transform.position = target.position - (transform.forward * zOffset) + (transform.up * yOffset) + (transform.right * xOffset);
        }
    }
}