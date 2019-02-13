using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraMovement { 

    public CameraFollow(GameObject _target) : base(_target)
    {

    }
    public override void LateUpdate(Transform cameraTransform)
    {
        base.LateUpdate(cameraTransform);
    }
}
