using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    bool isLiftingObject = false;

    [SerializeField]
    Transform levitateTransform;
    [SerializeField]
    float levDistance;
    [SerializeField]
    float levitateFollowSpeed = 3f;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float transfromMoveSpeed = 3f;

    private float xInput;
    private float yInput;
    private float zInput;

    private Vector3 levDirection;
    private Vector3 centerPoint;

    private void Update()
    {

        TelekinesisInputHandler();
        //LevitateTransformFollowCam();
        if (isLiftingObject == true)
        {
            LevitateObject(levitatableObj);
        }
        if(isLiftingObject == true && Input.GetButtonDown("Throw"))
        {
            ThrowObject();
        }
    }


    private void TelekinesisInputHandler()
    {
        if (Input.GetButtonDown("UseTele"))
        {
            UsePower(levitatableObj);
        }
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        GetObjectRigidBody(objectToLevitate);
        objectRigidBody.useGravity = false;
        objectToLevitate.layer = 11;
        objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
        objectRigidBody.velocity = Vector3.zero;        //Stops the object from 
        objectRigidBody.angularVelocity = Vector3.zero; //moving once you let it go
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        MoveObjectToTransform(objectRigidBody, objectTransfrom);
        Debug.Log("LevitatingObj");
    }

    private void ThrowObject()
    {
        objectRigidBody = levitatableObj.GetComponent<Rigidbody>();
        objectRigidBody.useGravity = true;
        objectRigidBody.AddForce(transform.forward * speed * 10);
        isLiftingObject = false;
        levitatableObj = null;
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

    //This is what actually moves the object towards the levitate point
    private void MoveObjectToTransform(Rigidbody objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.MovePosition(objTransform);
    }

    private void MoveLevitateTransform()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Mouse Y");
        zInput = Input.GetAxis("levZ");

        levDirection = new Vector3(xInput, yInput, zInput);
        levitateTransform.Translate(levDirection * transfromMoveSpeed * Time.deltaTime);
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
        isLiftingObject = false;
        objectRigidBody.useGravity = true;
    }


    public void UsePower(GameObject objToLevitate)
    {
        if (objToLevitate != null)
        {
            if (objToLevitate.tag == "LevitatableObject")
            {
                if (isLiftingObject)
                {
                    DropObject(objToLevitate);
                    levitatableObj = null;
                }
                else if (!isLiftingObject)
                {
                    isLiftingObject = true;
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
        if (!isLiftingObject)
        {
            levitatableObj = gameObject;
            Debug.Log(levitatableObj.name + " can be levitated.");
        }

    }

    private void ResetLevitatableObj()
    {
        if (!isLiftingObject)
        {
            levitatableObj = null;
        }
    }

    //Alternate
    //private void LevitateTransformFollowCam()
    //{
    //    Camera cam = Camera.main;
    //    centerPoint = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, levDistance));
    //    levitateTransform.position = centerPoint;
    //}

    //Subscribe to Event
    private void OnEnable()
    {
        DetectObject.LevObjectDetected += SetLevitatableObject;
        DetectObject.LevObjectGone += ResetLevitatableObj;
    }

    private void OnDisable()
    {
        DetectObject.LevObjectDetected -= SetLevitatableObject;
        DetectObject.LevObjectGone -= ResetLevitatableObj;
    }
}
