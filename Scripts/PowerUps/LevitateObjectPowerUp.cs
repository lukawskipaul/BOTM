using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitateObjectPowerUp : PowerUp {



    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    bool isLevitatingObject = false;

    [SerializeField]
    Transform levitateTransform;

    [SerializeField]
    float levitateFollowSpeed = 3f;

    public override string PowerName
    {
        get
        {
            return "Levitate Object";
        }
    }

    private void Update()
    {
        if (isLevitatingObject == true)
        {
            LevitateObject(levitatableObj);
        }
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        GetObjectRigidBody(objectToLevitate);
        objectRigidBody.useGravity = false;
        objectToLevitate.layer = 11;
        objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
        objectRigidBody.velocity = Vector3.zero;    //Stops the object from moving once you let it go
        objectRigidBody.angularVelocity = Vector3.zero;
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        MoveLevitateObject(objectToLevitate, objectTransfrom);
        Debug.Log("LevitatingObj");
    }

    private void GetObjectRigidBody(GameObject objToLevitate)
    {
        try
        {
            objectRigidBody = objToLevitate.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            Debug.Log("No rigidbody");
            throw;
        }
    }

    private void MoveLevitateObject(GameObject objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.transform.position = objTransform;
    }

    private void DropObject(GameObject objectToDrop)
    {
        try
        {
            objectRigidBody = objectToDrop.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            Debug.Log("No rigidbody");
            throw;
        }
        objectToDrop.layer = 0;
        isLevitatingObject = false;
        objectRigidBody.useGravity = true;        
    }

    public override void UsePower(GameObject objToLevitate)
    {
        if (objToLevitate != null)
        {
            if (objToLevitate.tag == "LevitatableObject")
            {
                if (isLevitatingObject)
                {
                    DropObject(objToLevitate);
                }
                else if (!isLevitatingObject)
                {
                    isLevitatingObject = true;
                }

            }
        }
        else
        {
            Debug.Log("No levitatable object");
        }
        
    }

    private void SetLevitatableObject(GameObject gameObject)
    {
        if (!isLevitatingObject)
        {
            levitatableObj = gameObject;
            Debug.Log(levitatableObj.name + " can be levitated.");
        }
        
    }

    private void ResetLevitatableObj(GameObject gameObject)
    {
        if (isLevitatingObject)
        {
            DropObject(levitatableObj);
        }
        levitatableObj = null;
    }

    private void OnEnable()
    {
        DetectObject.LevObjectDetected += SetLevitatableObject;
        DetectObject.LevObjectExit += ResetLevitatableObj;
    }

    private void OnDisable()
    {
        DetectObject.LevObjectDetected -= SetLevitatableObject;
        DetectObject.LevObjectExit += ResetLevitatableObj;
    }
}
