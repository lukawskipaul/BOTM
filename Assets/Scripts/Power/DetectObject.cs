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

    //Spherecast To detect objects We can use telekinesis on
    private void CastSphere()
    {
        Ray ray = new Ray(detectVector, transform.forward);
        RaycastHit hit;
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
