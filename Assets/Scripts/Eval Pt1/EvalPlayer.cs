using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalPlayer : MonoBehaviour {
    Rigidbody rb;
    public float moveSpeed = 1,turnSpeed = 0.2f;
	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody>();
        this.rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
	}
	
	// Update is called once per frame
	void Update () {
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.W))
            {
                MoveForwardOrBackward("forward");
            }
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.S))
            {
                MoveForwardOrBackward("backward");
            }
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.A))
            {
                TurnMovement("left");
            }
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.D))
            {
                TurnMovement("right");
            }
        
	}
    private void TurnMovement(string _direction)
    {
        switch (_direction.ToLower())
        {
            case "left":
                this.transform.Rotate(Vector3.up, -1 * (turnSpeed * Time.deltaTime));
                break;
            case "right":
                this.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                break;
        }
        
    }
    private void MoveForwardOrBackward(string _direction)
    {
        switch (_direction.ToLower())
        {
            case "forward":
                this.transform.Translate(0,0, 1 * moveSpeed * Time.deltaTime);
                break;
            case "backward":
                this.transform.Translate(0,0, -1 * moveSpeed * Time.deltaTime);
                break;
        }
    }
    
}
