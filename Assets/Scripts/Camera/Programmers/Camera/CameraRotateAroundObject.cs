using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAroundObject : CameraRotate
{

    
    public CameraRotateAroundObject(GameObject _target) : base(_target)
    {
        
    }

    public override void LateUpdate(Transform cameraTransform)
    {
        RotationFormula();

        base.LateUpdate(cameraTransform);

    }
    protected override void RotationFormula()
    {
        base.RotationFormula();
        MaintainCameraDistanceWhileRotating();
    }

}
