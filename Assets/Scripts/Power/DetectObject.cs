using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Event that is raised when an object is detected
    public static event Action<GameObject> LevObjectDetected;
    public static event Action<GameObject> EnemyObjDetected;
    // Event that is rasied when an object is no longer in sight
    public static event Action LevObjectGone;
    public static event Action EnemyObjGone;

    //A boolean that InputCameraChange modifies to inform this class to run the private method CastSphere
    public static bool EnemySearchNeeded;

    [SerializeField]
    Transform detectPoint;
    [SerializeField]
    float detectRadius, detectRange;

    Vector3 detectVector;

    private void Update()
    {
        detectVector = detectPoint.position;
        CastSphere("LevitatableObject");
        if (EnemySearchNeeded)
        {
            CastSphere("Enemy");
        }
    }

    //Spherecast To detect objects We can use telekinesis on, then do a Raycast along the path between the detectVector and the levitation object to make sure it's clear
    private void CastSphere(String FindTag)
    {
        Ray ray = new Ray(detectVector, transform.forward);
        RaycastHit hit;
        int IgnoreRayCastLayer = 1 << 2;
        int PlayerLayer = 1 << 9;
        int IgnoredLayerMask = ~((IgnoreRayCastLayer) | (PlayerLayer));
        if (Physics.SphereCast(ray, detectRadius, out hit, detectRange, IgnoredLayerMask))
        {
            if (hit.collider.gameObject.tag == FindTag)
            {
                bool pathToObjClear;
                RaycastHit nonTagObjHit;
                float distToLevObj = (hit.point - detectVector).magnitude;
                if (Physics.Raycast(detectVector, hit.point - detectVector, out nonTagObjHit, distToLevObj, IgnoredLayerMask))
                {
                    if (nonTagObjHit.collider.gameObject.tag != FindTag && nonTagObjHit.collider.gameObject.tag != "Checkpoint" && nonTagObjHit.collider.gameObject.tag != "Untagged")
                    {

                        Debug.Log("PathBlockedByObject: " + nonTagObjHit.collider.gameObject);
                        pathToObjClear = false;
                        //the path to the enemy is blocked, give up searching
                        if (FindTag == "Enemy") EnemySearchNeeded = false;
                    }
                    else
                    {
                        pathToObjClear = true;

                        Debug.Log(FindTag + "Detected");
                    }
                }
                else
                {
                    pathToObjClear = true;
                }
                if (pathToObjClear)
                {
                    if (FindTag == "LevitatableObject") OnLevObjectDetected(hit.collider.gameObject);
                    else if (FindTag == "Enemy")
                    {
                        OnEnemyObjDetected(hit.collider.gameObject);
                        //We found an enemy, we don't need to search anymore
                        EnemySearchNeeded = false;
                    }
                }
            }

        }
        else
        {
            Debug.Log("No" + FindTag);
            if (FindTag == "LevitatableObject") OnLevObjectGone();
            if (FindTag == "Enemy")
            {
                OnEnemyObjGone();
                //there was no enemy, don't keep searching
                EnemySearchNeeded = false;
            }
        }
    }

    // Publish Events

    private void OnEnemyObjDetected(GameObject detObj)
    {
        if (EnemyObjDetected != null)
        {
            EnemyObjDetected.Invoke(detObj);
        }
    }

    private void OnEnemyObjGone()
    {
        EnemyObjGone.Invoke();
    }

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
