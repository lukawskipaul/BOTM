using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCameraChange : MonoBehaviour

{
    [SerializeField] GameObject CM_LockOnCamera;
    [SerializeField] GameObject CM_LookAtTargetObject;
    

    private Animator anim;
    private bool lockOn;

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
                //TODO: Determine the best way to test for how far away the player is from the enemy, so that the lock on doesn't go an infinite distance.
                //Notes:
                //Continuously searching for an enemy with CastSphere the same way it looks for Levitatable Objects leads to a jerky camera and makes the lock on break when too close to the enemy.
                //The enemy's sword blocks locking on, since it lacks a tag, so CastSphere isn't told to ignore it like it's told to ignore checkpoints.
                //Moving the enemy sword to the IgnoreRaycast Layer makes the sword unable to hit the player.
                //Giving the enemy's sword the Enemy tag means the player would be able to lock onto it.
            }
            else ResetLockOnTarget();

        }
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
        //we found our LockOn Target, set it as what to target in the cinemachine object
        Cinemachine.CinemachineTargetGroup CM_TargetGroup = CM_LookAtTargetObject.GetComponent<Cinemachine.CinemachineTargetGroup>();
                CM_TargetGroup.m_Targets[1] = new Cinemachine.CinemachineTargetGroup.Target();
                CM_TargetGroup.m_Targets[1].weight = 1;
                CM_TargetGroup.m_Targets[1].radius = 1;
                CM_TargetGroup.m_Targets[1].target = gameObject.transform;
                LockCamera();
        
    }

    private void ResetLockOnTarget()
    {
        //remove the target from the cinemachine lock on list and unlocks the camera
        
        Cinemachine.CinemachineTargetGroup CM_TargetGroup = CM_LookAtTargetObject.GetComponent<Cinemachine.CinemachineTargetGroup>();
        CM_TargetGroup.m_Targets[1] = new Cinemachine.CinemachineTargetGroup.Target();
        UnLockCamera();
         

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