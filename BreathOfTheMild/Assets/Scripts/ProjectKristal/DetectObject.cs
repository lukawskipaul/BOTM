using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour {

    IActivatable activatableObject;

    // Events that are raised when an object is detected or object leaves detectionn area
    public static event Action<GameObject> LevObjectDetected;
    public static event Action<GameObject> LevObjectExit;

    private void Update()
    {
        HandleInput();
    }

    //We can also do a raycast
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "ActivatableObject")
        {
            Debug.Log("Collided with Activatable");
            activatableObject = other.gameObject.GetComponentInParent<IActivatable>();
        }
        if (other.tag == "LevitatableObject")
        {
            OnLevObjectDetected(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        activatableObject = null;

        if (other.tag == "LevitatableObject")
        {
            OnLevObjectExit(other.gameObject);
        }
        
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Activate"))
        {
            
            if (activatableObject != null)
            {
                activatableObject.DoActivate();
            }
        }

        //if (Input.GetButtonDown("UsePower"))
        //{
        //    if (PowerupManager.Instance.currentPower == PowerupManager.Instance.levitateObject)
        //    {
        //        PowerupManager.Instance.currentPower.UsePower(levitatableObj);
        //    }
        //}

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
