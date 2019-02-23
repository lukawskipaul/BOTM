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
    GameObject player;
    [SerializeField]
    Transform levitateTransform;
    [SerializeField]
    float throwForce = 1f;
    [SerializeField]
    float transfromMoveSpeed = 3f;
    [SerializeField]
    float telePushPullSpeed = 3f;
    [SerializeField]
    float maxSpeed = 1f;
    [SerializeField]
    float smoothtime = 1f;
    [SerializeField]
    float maxDistance = 20f;
    [SerializeField]
    float minDistance = 1f;

    private float baseLevitateFollowSpeed;
    private float xInput;
    private float yInput;
    private float zInput;

    BoxCollider boxCollider;

    private Vector3 levDirection;
    private Vector3 startingTransform;

    private Vector3 velocity = Vector3.zero;

    #endregion

    private void Start()
    {
        startingTransform = levitateTransform.localPosition;
    }

    private void Update()
    {

        TelekinesisInputHandler();
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
        OnTeleManualMovingObject();
        GetObjectRigidBody(objectToLevitate);
        if (objectRigidBody != null)
        {
            objectRigidBody.useGravity = false;
            objectToLevitate.layer = 10;
            objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
            objectRigidBody.velocity = Vector3.zero;        //Stops the object from 
            objectRigidBody.angularVelocity = Vector3.zero; //moving once you let it go
            Vector3 objectTransfrom = objectToLevitate.transform.position;
            MoveLevitateTransform();
            MoveObjectToTransform(objectRigidBody, objectTransfrom);
            CheckDistance();
            Debug.Log("LevitatingObj");
        }
        else
        {
            isLiftingObject = false;
            levitatableObj = null;
            levitateTransform.localPosition = startingTransform;
        }
    }

    private void ThrowObject()
    {
        objectRigidBody = levitatableObj.GetComponent<Rigidbody>();
        objectRigidBody.useGravity = true;
        objectRigidBody.AddForce(Camera.main.transform.forward * throwForce * 10);
        isLiftingObject = false;
        levitatableObj.tag = "ThrownObj";
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
            isLiftingObject = false;
            levitatableObj = null;
            levitateTransform.localPosition = startingTransform;
        }
    }

    //This is what actually moves the object towards the levitate point
    private void MoveObjectToTransform(Rigidbody objToLevitate, Vector3 objTransform)
    {
        //objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed);
        objTransform = Vector3.SmoothDamp(objTransform, levitateTransform.position, ref velocity, smoothtime, maxSpeed);
        objToLevitate.MovePosition(objTransform);
    }

    private void MoveLevitateTransform()
    {
        //xInput = Input.GetAxis("Mouse X");
        //yInput = Input.GetAxis("Mouse Y");

        
        zInput = Input.mouseScrollDelta.y * telePushPullSpeed;

        if ((Vector3.Distance(levitatableObj.transform.position, player.transform.position) <= minDistance))
        {
            zInput = 0;
        }

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
        }
        objectToDrop.layer = 0;
        isLiftingObject = false;
        objectRigidBody.useGravity = true;
        OnTeleStoppedManualMovingObject();
        levitateTransform.localPosition = startingTransform;
    }

    private void CheckDistance()
    {
        //Vector3 pos = levitateTransform.position;
        //pos.z = Mathf.Clamp(levitateTransform.position.z, 7, 25);
        //levitateTransform.position = pos;
        if (Vector3.Distance(levitatableObj.transform.position, player.transform.position) <= minDistance)
        {
            objectRigidBody.AddForce(Camera.main.transform.forward * throwForce * 10);
            //levitateTransform.position = objectRigidBody.position;
        }
        if (Vector3.Distance(levitatableObj.transform.position, player.transform.position) >= maxDistance)
        {
            DropObject(levitatableObj);
        }
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
