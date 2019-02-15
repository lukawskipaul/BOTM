using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCameraChange : MonoBehaviour

{
    [SerializeField] GameObject CM_LockOnCamera;

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
            lockOn = !lockOn;
            CM_LockOnCamera.SetActive(lockOn);
            anim.SetBool("LockOn", lockOn);
        }
    }
}