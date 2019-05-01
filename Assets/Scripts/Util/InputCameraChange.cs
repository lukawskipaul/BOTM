using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCameraChange : MonoBehaviour

{
    [SerializeField] GameObject CM_LockOnCamera;
    [SerializeField] GameObject CM_LookAtTargetObject;

    GameObject LockOnTarget;
    float DistToTarget;

    private Animator anim;
    public bool lockOn;
    [SerializeField]
    private float LockOnRange;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        lockOn = false;
        CM_LockOnCamera.SetActive(lockOn);
        anim.SetBool("LockOn", lockOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LockOn"))
        {
            if (!lockOn)
            {
                //if not locked on, tell DetectObject we're looking for an enemy, so it will run DetectObject.CastSphere("Enemy"), which will look for an enemy and set the need to search as false when it's done
                DetectObject.EnemySearchNeeded = true;
            }
            else ResetLockOnTarget();

        }
        //if we're currently locked on, check if we're too far away, and if so, unlock the camera
        if (lockOn)
        {
            if (!LockOnTarget.activeInHierarchy)
            {
                ResetLockOnTarget();
            }
            else {
                DistToTarget = Vector3.Distance(LockOnTarget.transform.position, transform.position);
                Cinemachine.CinemachineVirtualCamera CM_vcam = CM_LockOnCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
                var transposer = CM_vcam.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
                Vector3 TargetVector = new Vector3(.5f, 2.5f, .5f);
                if (Input.GetButtonDown("LockOnSwitch"))
                {
                    LockOnTarget.layer = LayerMask.NameToLayer("Ignore Raycast");
                    DetectObject.SearchDirection = Input.GetAxis("LockOnSwitch");
                    DetectObject.EnemySearchNeeded = true;

                }
                if (DistToTarget < 1.5)
                {
                    TargetVector = new Vector3(1.5f, 1.6f, -2f);
                }

                else if (DistToTarget < LockOnRange / 4)
                {
                    TargetVector = new Vector3(1.5f, 1.6f, -2f);
                }
                else if (DistToTarget < LockOnRange / 2)
                {
                    //float rotationDirection = -2f;
                    //if (Vector3.Angle(transform.forward, LockOnTarget.transform.forward) != 0) rotationDirection *= -1;
                    TargetVector = new Vector3(1.5f, 1.6f, -2f);
                }

                transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, TargetVector, .25f);

                if (DistToTarget > LockOnRange)
                {
                    transposer.m_FollowOffset = new Vector3(0, 1.8f, 0);
                    ResetLockOnTarget();
                }
            }
        }
        else
        {
            if (LockOnTarget != null) UnLockCamera();
        }
    }

    public GameObject GetLockOnTarget()
    {
        if (lockOn)
        {
            return LockOnTarget;
        }
        else return null;
    }


    private void UnLockCamera()
    {
        Debug.Log("CameraUnlocked");
        lockOn = false;
        CM_LockOnCamera.SetActive(lockOn);
        anim.SetBool("LockOn", lockOn);

    }

    private void LockCamera()
    {
        Debug.Log("Camera Locked");
        lockOn = true;
        CM_LockOnCamera.SetActive(lockOn);
        anim.SetBool("LockOn", lockOn);
    }

    private void SetLockOnTarget(GameObject gameObject)
    {
        if (LockOnTarget != null)
        {
            if(LockOnTarget.layer == LayerMask.NameToLayer("Ignore Raycast"))
            {
                LockOnTarget.layer = LayerMask.NameToLayer("Enemies");
            }
        }
        //we found our LockOn Target, set it as what to target in the cinemachine object
        Cinemachine.CinemachineTargetGroup CM_TargetGroup = CM_LookAtTargetObject.GetComponent<Cinemachine.CinemachineTargetGroup>();
        CM_TargetGroup.m_Targets[1] = new Cinemachine.CinemachineTargetGroup.Target();
        CM_TargetGroup.m_Targets[1].weight = 2;
        CM_TargetGroup.m_Targets[1].radius = 1;
        CM_TargetGroup.m_Targets[1].target = gameObject.transform;
        LockOnTarget = gameObject;
        LockCamera();

    }

    private void ResetLockOnTarget()
    {
        bool BreakLock = false;
        //remove the target from the cinemachine lock on list and unlocks the camera
        if (LockOnTarget != null)
        {
            if (LockOnTarget.layer == LayerMask.NameToLayer("Ignore Raycast"))
            {
                LockOnTarget.layer = LayerMask.NameToLayer("Enemies");
                BreakLock = false;
            }
            else BreakLock = true;
        }
        if (BreakLock)
        {
            Cinemachine.CinemachineTargetGroup CM_TargetGroup = CM_LookAtTargetObject.GetComponent<Cinemachine.CinemachineTargetGroup>();
            CM_TargetGroup.m_Targets[1] = new Cinemachine.CinemachineTargetGroup.Target();
            UnLockCamera();
        }

    }

    private void OnEnable()
    {
        DetectObject.EnemyObjDetected += SetLockOnTarget;
        DetectObject.EnemyObjGone += ResetLockOnTarget;
    }

    private void OnDisable()
    {
        DetectObject.EnemyObjDetected -= SetLockOnTarget;
        DetectObject.EnemyObjGone -= ResetLockOnTarget;
    }
}