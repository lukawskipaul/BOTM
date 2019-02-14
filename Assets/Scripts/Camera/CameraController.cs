using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    CameraRotateObject cameraRotate;
    CameraFollow cameraFollow;
    CameraRotateVertical cameraRotVertical;

    public GameObject target;
    public float CameraRotationXSpeed = 5.0f;
    public float CameraRotationYSpeed = 1.0f;
    public bool lookAtObject = true, ActivateRotation = true, follow = true, verticalcontrol = true, InvertY = false;
    // Use this for initialization
    void Start () {
        cameraRotate = new CameraRotateObject(target);
        cameraFollow = new CameraFollow(target);
        cameraRotVertical = new CameraRotateVertical(target);

        cameraFollow.Start(this.transform);
        cameraRotate.Start(this.transform);
        cameraRotVertical.Start(this.transform);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (follow)
        {
            cameraFollow.LateUpdate(this.transform);
        }
        if (ActivateRotation)
        {
            //For Designers
            cameraRotate.CameraRotationSpeed = CameraRotationXSpeed;
            cameraRotate.targetRotationSpeed = CameraRotationXSpeed;
            cameraRotate.RotateDirection = Input.GetAxis("Mouse X");
            cameraRotate.LateUpdate(this.transform);
        }
        if (verticalcontrol)
        {
            cameraRotVertical.CameraRotationSpeed = CameraRotationYSpeed;
            cameraRotVertical.VerticalRotation = Input.GetAxis("Mouse Y");
            cameraRotVertical.VerticalRotation = (InvertY) ? Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y") * -1;


            cameraRotVertical.LateUpdate(this.transform);
        }
        if (lookAtObject)
        {
            cameraRotate.LookAtObject(this.transform);
        }
        

    }
}
