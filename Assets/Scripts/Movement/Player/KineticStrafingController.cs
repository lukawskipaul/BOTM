using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticStrafingController : MonoBehaviour {
    KineticStrafing strafing;
    [Tooltip("How fast to move an object")]
    public float moveSpeed = 1;
    Rigidbody rigidbody;
    public static Vector3 playerdir;
    // Use this for initialization
    void Start () {
        strafing = new KineticStrafing();
        strafing.Direction = Vector3.zero;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
	
	// Update is called once per frame
	void Update () {

        UpdateDirectionInput();

        //For Designers
        strafing.Speed = UpdateMoveSpeed(moveSpeed);

        this.strafing.UpdateMovement(gameObject);
	}

    public void UpdateDirectionInput()
    {

         strafing.Direction = Vector3.zero;

        if (KeyboardInputUtil.IsHoldingKey(KeyCode.W))
        {
            strafing.Direction += new Vector3(0, 0, 1);
        }
        if (KeyboardInputUtil.IsHoldingKey(KeyCode.S))
        {
            strafing.Direction += new Vector3(0, 0, -1);
        }
        if (KeyboardInputUtil.IsHoldingKey(KeyCode.A))
        {
            strafing.Direction += new Vector3(-1, 0, 0);
        }
        if (KeyboardInputUtil.IsHoldingKey(KeyCode.D))
        {
            strafing.Direction += new Vector3(1, 0, 0);
        }
        playerdir = strafing.Direction;
    }
    public float UpdateMoveSpeed(float _speed)
    {
        if (strafing.Direction == new Vector3(1,0,1) || strafing.Direction == new Vector3(1, 0, -1) || strafing.Direction == new Vector3(-1, 0, 1) || strafing.Direction == new Vector3(-1, 0, -1))
        {
            return _speed * 2/ 3;
        }
        return _speed;
    }
}
