using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Telekinesis : MonoBehaviour
{
    #region Variables
    public static event Action TeleManualMovingObject;
    public static event Action TeleStoppedManualMovingObject;

    Rigidbody objectRigidBody;
    private GameObject levitatableGO;
    public bool isLiftingObject = false;

    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform levitateTransform;
    [SerializeField]
    float throwForce = 1f;
    [SerializeField]
    float transfromMoveSpeed = 3f;
    [SerializeField]
    [Range(.5f, 2f)]
    float telePushPullSpeed = 1f;
    [SerializeField]
    float maxSpeed = 1f;
    [SerializeField]
    float smoothtime = 1f;
    [SerializeField]
    float maxDistance = 20f;
    [SerializeField]
    float minDistance = 1f;

    [SerializeField]
    private GameObject journalMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject crosshairPanel;

    private float baseLevitateFollowSpeed;
    private float xInput;
    private float yInput;
    private float zInput;

    private Animator anim;
    private BoxCollider boxCollider;

    private Vector3 levDirection;
    private Vector3 startingTransform;

    private Vector3 velocity = Vector3.zero;

    private TKObject currentTKObject;


    private float currentEnergy = 100f;

    [SerializeField]
    Slider energySlider;

    [SerializeField]
    float maxEnergy = 100f;

    [SerializeField]
    private float energyDrainRate = 1f, energyRechargeRate = 10f;
    private const string telekinesisButtonName = "UseTele";
    private const string tkThrowButtonName = "Throw";
    private const string telekinesisBooleanName = "isUsingTelekinesis";
    private const string telekinesisThrowName = "isDoingTKThrow";

    #endregion

    private void Start()
    {
        startingTransform = levitateTransform.localPosition;

        energySlider.value = EnergyPercent();

        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        TelekinesisInputHandler();

        if (isLiftingObject == true && Input.GetButtonDown("Throw"))
        {
            ThrowObject();
            //AkSoundEngine.PostEvent("Stop_Tk", gameObject);
            AkSoundEngine.PostEvent("Play_TK_Throw", gameObject);

        }
        else if (isLiftingObject == false)
        {
            currentEnergy += (energyRechargeRate * Time.deltaTime);
        }
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        energySlider.value = EnergyPercent();
    }

    private void FixedUpdate()
    {
        if (isLiftingObject)
        {
            LevitateObject(levitatableGO);
        }
    }

    private void TelekinesisInputHandler()
    {
        if (Input.GetButtonDown("UseTele"))
        {
            UsePower(levitatableGO);
            //AkSoundEngine.PostEvent("Play_TK", gameObject);
        }
    }

    private void LevitateObject(GameObject objectToLevitate)
    {
        currentEnergy -= (energyDrainRate * Time.deltaTime);
        OnTeleManualMovingObject();
        GetObjectRigidBody(objectToLevitate);
        GetObjectTKObject(objectToLevitate);

        if (objectRigidBody != null)
        {
            /* Play telekinesis animation when telekinesis button is pressed */
            if (!journalMenu.gameObject.activeInHierarchy && !pauseMenu.gameObject.activeInHierarchy)
            {
                anim.SetBool(telekinesisBooleanName, true);
            }

            objectRigidBody.useGravity = false;
            //objectToLevitate.layer = 10;
            objectRigidBody.rotation = Quaternion.Euler(0, 0, 0);
            objectRigidBody.velocity = Vector3.zero;        //Stops the object from 
            objectRigidBody.angularVelocity = Vector3.zero; //moving once you let it go
            Vector3 objectTransfrom = objectToLevitate.transform.position;
            MoveLevitateTransform();
            MoveObjectToTransform(objectRigidBody, objectTransfrom);
            currentTKObject.SetLevitating();
            CheckDistance();
            if (currentEnergy <= 0)
            {
                DropObject();
            }
        }
        else
        {
            isLiftingObject = false;
            levitatableGO = null;
            levitateTransform.localPosition = startingTransform;
            anim.SetBool(telekinesisThrowName, false);
            anim.SetBool(telekinesisBooleanName, false);

        }
    }

    private void ThrowObject()
    {
        /* Play telekinesis throw animation when throw button is pressed */
        if (!journalMenu.gameObject.activeInHierarchy && !pauseMenu.gameObject.activeInHierarchy)
        {
            anim.SetBool(telekinesisThrowName, true);
            anim.SetBool(telekinesisBooleanName, false);

            //actual code for moving the object is in an animation event
        }
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
            ResetTK();
        }
    }

    private void GetObjectTKObject(GameObject objToLevitate)
    {
        try
        {
            currentTKObject = objToLevitate.GetComponent<TKObject>();
        }
        catch (System.Exception)
        {
            Debug.Log("No TKObject");
            ResetTK();
        }
    }

    //This is what actually moves the object towards the levitate point
    private void MoveObjectToTransform(Rigidbody objToLevitate, Vector3 objTransform)
    {
        objTransform = Vector3.SmoothDamp(objTransform, levitateTransform.position, ref velocity, smoothtime, maxSpeed);
        objToLevitate.MovePosition(objTransform);
    }

    private void MoveLevitateTransform()
    {
        //zInput = Input.mouseScrollDelta.y * telePushPullSpeed;
        zInput = Input.GetAxis("PushPull") * (telePushPullSpeed * .01f);

        if ((Vector3.Distance(levitatableGO.transform.position, player.transform.position) <= minDistance))
        {
            zInput = 0;
        }

        levDirection = new Vector3(0, 0, zInput);
        //levDirection = new Vector3(levitateTransform.position.x, levitateTransform.position.y, zInput);
        levitateTransform.Translate(levDirection * transfromMoveSpeed * Time.deltaTime);
    }

    public void DropObject()
    {
        if (isLiftingObject)
        {
            /* Stop playing telekinesis animation when object is dropped */
            anim.SetBool(telekinesisBooleanName, false);
            anim.SetBool(telekinesisThrowName, false);

            objectRigidBody.useGravity = true;
            currentTKObject.SetNeutral();
            ResetTK();
        }
    }

    private void CheckDistance()
    {
        Bounds LevGOBounds = levitatableGO.GetComponent<Collider>().bounds;
        Vector3 LevGOExtents = LevGOBounds.extents;
        Vector3 FacePoint = LevGOBounds.center - (levitateTransform.forward * LevGOExtents.magnitude / Mathf.Sqrt(3)); //I did math, don't ask where Sqrt(3) came from
        Vector3 PointOnPlayer = player.GetComponent<Collider>().bounds.ClosestPoint(FacePoint);
        if (Vector3.Distance(FacePoint, PointOnPlayer) < minDistance)
        {
            objectRigidBody.MovePosition(levitatableGO.transform.position + (levitateTransform.forward * minDistance) * Time.deltaTime);
            levitateTransform.position = levitatableGO.transform.position;
            zInput = 0;
        }
        if (Vector3.Distance(levitatableGO.transform.position, player.transform.position) >= maxDistance)
        {
            DropObject();
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
                    DropObject();
                }
                else if (!isLiftingObject)
                {
                    isLiftingObject = true;
                }
            }
        }
    }

    private void SetLevitatableObject(GameObject gameObject)
    {
        if (!isLiftingObject)
        {
            levitatableGO = gameObject;

            //TEMPORARY FEEDBACK
            levitatableGO.GetComponent<Renderer>().material.color = Color.green;
        }

    }

    private void ResetLevitatableObj()
    {
        //TEMPORARY FEEDBACK
        if (levitatableGO != null)
        {
            levitatableGO.GetComponent<Renderer>().material.color = Color.white;
        }

        if (!isLiftingObject)
        {
            levitatableGO = null;
        }
    }

    private void ResetTK()
    {
        isLiftingObject = false;
        levitatableGO = null;
        currentTKObject = null;
        OnTeleStoppedManualMovingObject();
        levitateTransform.localPosition = startingTransform;
        anim.SetBool(telekinesisThrowName, false);
        anim.SetBool(telekinesisBooleanName, false);
    }

    float EnergyPercent()
    {
        return currentEnergy / maxEnergy;
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
        PlayerHealth.TakeDamage += DropObject;
    }

    private void OnDisable()
    {
        DetectObject.LevObjectDetected -= SetLevitatableObject;
        DetectObject.LevObjectGone -= ResetLevitatableObj;
        PlayerHealth.TakeDamage -= DropObject;
    }
    #endregion

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

    /* Called during specific attack animation frame to start doing damage to hit enemies */
    public void TelekinesisThrow()
    {
        objectRigidBody = levitatableGO.GetComponent<Rigidbody>();
        objectRigidBody.useGravity = true;
        objectRigidBody.AddForce(Camera.main.transform.forward * throwForce * 10);
        isLiftingObject = false;
        currentTKObject.SetThrown();
        levitatableGO = null;
        anim.SetBool(telekinesisBooleanName, false);
    }

    #endregion
}
