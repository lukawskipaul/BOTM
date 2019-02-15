using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRayCastRotate : MonoBehaviour {
    enum TurnState { Right, Left, Stop }
    public GameObject target;
    private static TurnState state,prevstate;
    public float Rotspeed = 10;
    int layermask = 1 << 9;
    private float prevdistance;
    Vector3 forwardright, forwardleft, backleft, backright;
    // Use this for initialization
    void Start () {
        state = TurnState.Right;
        forwardleft = Vector3.left + Vector3.forward;
        forwardright = Vector3.right + Vector3.forward;
        backleft = Vector3.back + Vector3.left;
        backright = Vector3.back + Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(backleft) * 100, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.left) * 100, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(forwardleft) * 100, Color.red);
        Debug.DrawLine(this.transform.position,this.transform.TransformDirection(Vector3.forward) * 100,Color.green);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(forwardright) * 100, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.right) * 100, Color.red);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(backright) * 100, Color.red);


        Debug.DrawLine(this.transform.position,target.transform.position,Color.blue);
        DetectTarget();
        Changestate();
    }
    private void DetectTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity, layermask))
        {
            state = prevstate = TurnState.Left;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
        }
        else if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(forwardleft), out hit, Mathf.Infinity, layermask))
        {
            state = prevstate = TurnState.Left;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(forwardleft) * hit.distance, Color.yellow);
        }
        else if (Physics.Raycast(this.transform.position,this.transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity,layermask))
        {
            state = TurnState.Stop;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(forwardright), out hit, Mathf.Infinity, layermask))
        {
            state = prevstate = TurnState.Right;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(forwardright) * hit.distance, Color.yellow);
        }
        else if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, layermask))
        {
            state = prevstate = TurnState.Right;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
        }
        else
        {
            state = prevstate;
        }
        
    }
    private void Changestate()
    {
        switch (state)
        {
            case TurnState.Right:
                this.transform.Rotate(Vector3.up * Rotspeed * Time.deltaTime);
                break;
            case TurnState.Left:
                this.transform.Rotate(-(Vector3.up * Rotspeed * Time.deltaTime));
                break;
            case TurnState.Stop:
                break;
        }
        Debug.Log(state);
    }
}
