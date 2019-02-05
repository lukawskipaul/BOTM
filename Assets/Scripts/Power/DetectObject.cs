using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Event that is raised when an object is detected
    public static event Action<GameObject> LevObjectDetected;
    // Event that is rasied when an object is no longer in sight
    public static event Action LevObjectGone;

    [SerializeField]
    Transform detectPoint;
    [SerializeField]
    float detectRadius, detectRange;

    Vector3 detectVector;

    private void Update()
    {
        detectVector = detectPoint.position;
        CastSphere();
    }

    //Spherecast To detect objects We can use telekinesis on, then do a Raycast along the path between the detectVector and the levitation object to make sure it's clear
    private void CastSphere()
    {
        Ray ray = new Ray(detectVector, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, detectRadius, out hit, detectRange))
        {
            if (hit.collider.gameObject.tag == "LevitatableObject")
            {
                bool pathToObjClear;
                RaycastHit nonLevObjHit;
                float distToLevObj = (hit.point - detectVector).magnitude;
                if (Physics.Raycast(detectVector, hit.point - detectVector, out nonLevObjHit, distToLevObj))
                {
                    if (nonLevObjHit.collider.gameObject.tag != "LevitatableObject" && nonLevObjHit.collider.gameObject.tag != "Checkpoint")
                    {
                        Debug.Log("PathBlockedByNonLevObject");
                        pathToObjClear = false;
                    }
                    else
                    {
                        pathToObjClear = true;
                       
                        Debug.Log("TeleObjDetected");
                    }
                }
                else
                {
                    pathToObjClear = true;
                }
                if (pathToObjClear)
                {
                    OnLevObjectDetected(hit.collider.gameObject);
                }
            }
            
        }
        else
        {
            Debug.Log("NoTeleObj");
            OnLevObjectGone();
        }
    }

    // Publish Events
    private void OnLevObjectDetected(GameObject detObj)
    {
        if (LevObjectDetected != null)
        {
            LevObjectDetected.Invoke(detObj);
        }
    }

    private void OnLevObjectGone()
    {
        if (LevObjectGone != null)
        {
            LevObjectGone.Invoke();
        }
    }

}
