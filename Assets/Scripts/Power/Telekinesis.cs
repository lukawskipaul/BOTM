using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{

    public static event Action TeleManualMovingObject;
    public static event Action TeleStoppedManualMovingObject;

    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    public GameObject player;
    bool isLiftingObject = false;
    bool isManuallyMovingObj = false;

    [SerializeField]
    Transform levitateTransform;
    [SerializeField]
    float levitateFollowSpeed = 3f;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float transfromMoveSpeed = 3f;
    [SerializeField]
    float closeDistance;

    private float baseLevitateFollowSpeed;
    private float xInput;
    private float yInput;
    private float zInput;

    BoxCollider boxCollider;

    private Vector3 levDirection;
    private Vector3 centerPoint;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = levitateTransform.localPosition;
        baseLevitateFollowSpeed = levitateFollowSpeed;
    }

    private void Update()
    {

        TelekinesisInputHandler();
        //LevitateTransformFollowCam();
        if (isLiftingObject && !isManuallyMovingObj)
        {
            LevitateObject(levitatableObj);
        }
        else if(isLiftingObject && isManuallyMovingObj)
        {
            LevitateObject(levitatableObj);
            MoveLevitateTransform();
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
        if (Input.GetButtonDown("ManualTele"))
        {
            if (isLiftingObject)
            {
                if (isManuallyMovingObj)
                {
                    isManuallyMovingObj = false;
                    ResetAfterManualMove();
                    //OnTeleStoppedManualMovingObject();
                }
                else
                {
                    isManuallyMovingObj = true;
                    //OnTeleManualMovingObject();
                }
            }
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
        MoveObjectToTransform(objectRigidBody, objectTransfrom);
        SetSpeed();
        Debug.Log("LevitatingObj");
    }

    private void ThrowObject()
    {
        objectRigidBody = levitatableObj.GetComponent<Rigidbody>();
        objectRigidBody.useGravity = true;
        objectRigidBody.AddForce(Camera.main.transform.forward * speed * 10);
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
        //xInput = Input.GetAxis("Mouse X");
        //yInput = Input.GetAxis("Mouse Y");
        zInput = Input.mouseScrollDelta.y * 2;


        //levitateTransform.SetParent(null);

        levDirection = new Vector3(xInput, yInput, zInput);
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
        if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("pressing w");
            levitateFollowSpeed = 15;
        }
        else
        {
            levitateFollowSpeed = baseLevitateFollowSpeed;
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
