using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Events that are raised when an object is detected or object leaves detectionn area
    public static event Action<GameObject> LevObjectDetected;
    public static event Action<GameObject> LevObjectExit;

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

    //SpherecastToDetect
    private void CastSphere()
    {
        Ray ray = new Ray(detectVector, transform.forward);
        RaycastHit hit;
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(detectVector, detectRadius);
        if (Physics.SphereCast(ray, detectRadius, out hit, detectRange))
        {
            if (hit.collider.gameObject.tag == "LevitatableObject")
            {
                OnLevObjectDetected(hit.collider.gameObject);
                Debug.Log("TeleObjDetected");
            }
            
        }
        else
        {
            Debug.Log("NoTeleObj");
        }
    }


    private void OnLevObjectDetected(GameObject detObj)
    {
        if (LevObjectDetected != null)
        {
            LevObjectDetected.Invoke(detObj);
        }
    }

    private void OnLevObjectExit(GameObject detObj)
    {
        if (LevObjectDetected != null)
        {
            LevObjectExit.Invoke(detObj);
        }
    }
}
