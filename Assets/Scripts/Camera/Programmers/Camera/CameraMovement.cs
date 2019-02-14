using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : ICameraMovement {
    public GameObject targetObject { get; set; }
    protected static Vector3 cameraDistance { get; set; }
    int layermask = 1 << 9;
    float moveCameraPercent = .347f;
    public CameraMovement(GameObject _target)
    {
        targetObject = _target;
    }
    public virtual void Start(Transform cameraTransform)
    {
        layermask = ~layermask;
        cameraDistance = cameraTransform.position - targetObject.transform.position;
       
    }
    public virtual void LateUpdate(Transform cameraTransform)
    {
        cameraTransform.position = targetObject.transform.position + cameraDistance;
        CameraCollision(cameraTransform);
        //Debug.DrawLine(cameraTransform.position, cameraTransform.position - cameraDistance,Color.red);
    }
    private void CameraCollision(Transform cameraTransform)
    {
        RaycastHit hit;
        if (Physics.Linecast(cameraTransform.position,targetObject.transform.position,out hit,layermask))
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetObject.transform.position, (hit.distance * moveCameraPercent));
        }
    }
}
