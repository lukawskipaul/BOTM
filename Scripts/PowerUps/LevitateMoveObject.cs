using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevitateMoveObject : PowerUp
{
    public static event Action TeleMovingObject;
    public static event Action TeleStoppedMovingObject;



    Rigidbody objectRigidBody;
    public GameObject levitatableObj;
    GameObject levitatingObj;
    bool isLevitatingObject = false;
    bool isRotating = false;
    bool wasLevitating = false;
    bool isPulling = false;

    [SerializeField]
    Slider energySlider;

    [SerializeField]
    Transform levitateTransform, MinDistRayTransform;

    [SerializeField]
    float levitateFollowSpeed = 3f;

    [SerializeField]
    float transfromMoveSpeed = 3f, rotationAngleSnap = 15f;

    private float teleEnergy = 100f;

    [SerializeField]
    float maxEnergy = 100f;

    [SerializeField]
    private float energyDrainRate = 1f;

    [SerializeField]
    private float energyRechargeRate = 10f, maxDist = 5f, minDist = 1f;

    [SerializeField]
    GameObject player;

    [SerializeField]
    LayerMask levitatingObjLayer;

    private float xInput;
    private float yInput;
    private float zInput;

    private Vector3 levDirection;
    private Vector3 startingTransform;

    public override string PowerName
    {
        get
        {
            return "Telekinesis";
        }
    }

    private void Start()
    {
        startingTransform = levitateTransform.localPosition;
        energySlider.value = EnergyPercent();
    }

    private void Update()
    {
        Debug.Log(isPulling);
        if (isLevitatingObject == true)
        {
            if (Vector3.Distance(player.transform.position, levitatingObj.transform.position) > maxDist || teleEnergy <= 0)
            {
                if (isPulling && teleEnergy > 0)
                {
                    MoveLevitateObject(levitatableObj, new Vector3(levitatableObj.transform.position.x, levitatableObj.transform.position.y, levitatableObj.transform.position.z));
                    if (Vector3.Distance(player.transform.position, levitatingObj.transform.position) < maxDist)
                    {
                        isPulling = false;
                    }

                }
                else
                {
                    DropObject(levitatableObj);
                }

            }
            else
            {
                if (Input.GetButtonDown("ToggleAlternateAbility"))
                {
                    if (isRotating)
                    {
                        isRotating = false;
                    }
                    else
                    {
                        isRotating = true;
                    }
                }
                LevitateObject(levitatableObj);
                isPulling = false;
            }

        }
        else if (isLevitatingObject == false)
        {
            teleEnergy += (energyRechargeRate * Time.deltaTime);
        }
        teleEnergy = Mathf.Clamp(teleEnergy, 0, maxEnergy);
        energySlider.value = EnergyPercent();
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        if (!wasLevitating)
        {
            GetObjectRigidBody(objectToLevitate);
            objectRigidBody.useGravity = false;
            objectToLevitate.layer = 11;
            //objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
            objectRigidBody.velocity = Vector3.zero;    //Stops the object from moving once you let it go
            objectRigidBody.angularVelocity = Vector3.zero;
            wasLevitating = true;
            levitatingObj = objectToLevitate;
        }
        Vector3 objectTransfrom = objectToLevitate.transform.position;
        OnTeleMovingObject();
        if (isRotating)
        {
            RotateObject();
        }
        else
        {
            MoveLevitateTransform();
            MoveLevitateObject(objectToLevitate, objectTransfrom);
        }

        teleEnergy -= (energyDrainRate * Time.deltaTime);
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

            throw new UnityException("No rigidbody!");
        }
    }

    private void MoveLevitateObject(GameObject objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.Lerp(objTransform, levitateTransform.position, levitateFollowSpeed * Time.deltaTime);
        objToLevitate.transform.position = objTransform;
    }

    private void MoveLevitateTransform()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Mouse Y");
        zInput = Input.GetAxis("levZ");


        if (Physics.Raycast(MinDistRayTransform.position, Vector3.forward, minDist, levitatingObjLayer))
        {
            levDirection = new Vector3(xInput, yInput, levitatableObj.transform.position.z);
        }
        else
        {
            levDirection = new Vector3(xInput, yInput, zInput);
        }

        levitateTransform.Translate(levDirection * transfromMoveSpeed * Time.deltaTime);
    }

    private void RotateObject()
    {
        if (Input.GetButtonDown("Vertical"))
        {


            if (Input.GetAxis("Vertical") > 0)
            {
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), rotationAngleSnap);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(1, 0, 0), -rotationAngleSnap);
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), -rotationAngleSnap);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                levitatableObj.transform.RotateAround(levitatableObj.transform.position, new Vector3(0, 1, 0), rotationAngleSnap);
            }
        }


    }

    private void DropObject(GameObject objectToDrop)
    {
        try
        {
            objectRigidBody = objectToDrop.GetComponent<Rigidbody>();
        }
        catch (System.Exception)
        {
            throw new UnityException("No rigidbody!");
        }
        objectToDrop.layer = 0;
        isLevitatingObject = false;
        isRotating = false;
        objectRigidBody.useGravity = true;
        ResetLevTransform();
        levitatingObj = null;
        wasLevitating = false;
        OnTeleStoppedMovingObject();
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
                    isPulling = true;
                    levitatingObj = objToLevitate;
                }

            }
        }
        else
        {
            Debug.Log("No levitatable object");
        }

    }

    public void SetLevitatableObject(GameObject gameObject)
    {
        if (!isLevitatingObject)
        {
            levitatableObj = gameObject;
            Debug.Log(levitatableObj.name + " can be levitated.");
        }

    }

    private void RemoveLevitatableObj(GameObject gameObject)
    {
        if (isLevitatingObject)
        {
            DropObject(levitatableObj);
        }
        levitatableObj = null;
    }

    private void ResetLevTransform()
    {
        levitateTransform.localPosition = startingTransform;
    }

    float EnergyPercent()
    {
        return teleEnergy / maxEnergy;
    }

    private void OnTeleMovingObject()
    {
        if (TeleMovingObject != null)
        {
            TeleMovingObject.Invoke();
        }
    }

    private void OnTeleStoppedMovingObject()
    {
        if (TeleStoppedMovingObject != null)
        {
            TeleStoppedMovingObject.Invoke();
        }
    }

    private void OnEnable()
    {
        DetectObject.LevObjectDetected += SetLevitatableObject;
        //DetectObject.LevObjectExit += ResetLevitatableObj;
    }

    private void OnDisable()
    {
        DetectObject.LevObjectDetected -= SetLevitatableObject;
        //DetectObject.LevObjectExit += ResetLevitatableObj;
    }
}