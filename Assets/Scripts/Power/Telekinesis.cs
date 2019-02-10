using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{   
    #region Variables
    public static event Action TeleManualMovingObject;
    public static event Action TeleStoppedManualMovingObject;

    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    bool isLiftingObject = false;

    [SerializeField]
    Transform levitateTransform;
    [SerializeField]
    [Range(.01f, 1f)]
    float levitateFollowSpeed = .5f;
    [SerializeField]
    float throwForce = 1f;
    [SerializeField]
    float transfromMoveSpeed = 3f;
    [SerializeField]
    float telePushPullSpeed = 3f;

    private float baseLevitateFollowSpeed;
    private float xInput;
    private float yInput;
    private float zInput;

    BoxCollider boxCollider;

    private Vector3 levDirection;
    private Vector3 centerPoint;
    private Vector3 startingPosition;

    #endregion
    private void Start()
    {
        startingPosition = levitateTransform.localPosition;
        baseLevitateFollowSpeed = levitateFollowSpeed;
    }

    private void Update()
    {

        TelekinesisInputHandler();
        //LevitateTransformFollowCam();
        if (isLiftingObject == true && Input.GetButtonDown("Throw"))
        {
            ThrowObject();
        }


    }

    private void FixedUpdate()
    {
        if (isLiftingObject)
        {
            LevitateObject(levitatableObj);
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
        objectToLevitate.layer = 10;
        objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
        objectRigidBody.velocity = Vector3.zero;        //Stops the object from 
        objectRigidBody.angularVelocity = Vector3.zero; //moving once you let it go
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        MoveLevitateTransform();
        MoveObjectToTransform(objectRigidBody, objectTransfrom);
        SetSpeed();
        Debug.Log("LevitatingObj");
    }

    private void ThrowObject()
    {
        objectRigidBody = levitatableObj.GetComponent<Rigidbody>();
        objectRigidBody.useGravity = true;
        objectRigidBody.AddForce(Camera.main.transform.forward * throwForce * 10);
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
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed);
        objToLevitate.MovePosition(objTransform);
    }

    private void MoveLevitateTransform()
    {
        //xInput = Input.GetAxis("Mouse X");
        //yInput = Input.GetAxis("Mouse Y");
        zInput = Input.mouseScrollDelta.y * telePushPullSpeed;


        //levitateTransform.SetParent(null);

        levDirection = new Vector3(0, 0, zInput);
        //levDirection = new Vector3(levitateTransform.position.x, levitateTransform.position.y, zInput);
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

    private void SetSpeed()
    {
        //if (Vector3.Distance(player.transform.position, levitatableObj.transform.position) <= closeDistance)
        //{
        //    levitateFollowSpeed *= 2;
        //}
        //else if (Vector3.Distance(player.transform.position, levitatableObj.transform.position) > closeDistance)
        //{
        //    levitateFollowSpeed = baseLevitateFollowSpeed;
        //}
        //if (Input.GetButtonDown("Vertical"))
        //{
        //    if(levitateFollowSpeed < 1)
        //    {
                
        //    }
        //}
        //else
        //{
        //    if (levitateFollowSpeed > baseLevitateFollowSpeed)
        //    {
                
        //    }
        //    //levitateFollowSpeed = baseLevitateFollowSpeed;
        //}
    }

    private void SetLevitatableObject(GameObject gameObject)
    {
        if (!isLiftingObject)
        {
            levitatableObj = gameObject;

            //TEMPORARY FEEDBACK
            levitatableObj.GetComponent<Renderer>().material.color = Color.green;

            Debug.Log(levitatableObj.name + " can be levitated.");
        }

    }

    private void ResetLevitatableObj()
    {
        //TEMPORARY FEEDBACK
        if (levitatableObj != null)
        {
            levitatableObj.GetComponent<Renderer>().material.color = Color.white;
        }

        if (!isLiftingObject)
        {
            levitatableObj = null;
        }
    }

    private void ResetAfterManualMove()
    {
        levitateTransform.SetParent(Camera.main.gameObject.transform);
        levitateTransform.localPosition = startingPosition;
    }

    //Alternate way to move object to center of screen
    //private void LevitateTransformFollowCam()
    //{
    //    Camera cam = Camera.main;
    //    centerPoint = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, levDistance));
    //    levitateTransform.position = centerPoint;
    //}

    private void OnTeleManualMovingObject()
    {
        if (TeleManualMovingObject != null)
        {
            TeleManualMovingObject.Invoke();
        }
    }

    private void OnTeleStoppedManualMovingObject()
    {
        if (TeleStoppedManualMovingObject != null)
        {
            TeleStoppedManualMovingObject.Invoke();
        }
    }

    #region Event Subscribing
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
    #endregion
}
