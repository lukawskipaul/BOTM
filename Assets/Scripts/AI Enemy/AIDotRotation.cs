using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDotRotation : MonoBehaviour {

    enum TurnState { Right, Left, Stop }
    public GameObject target;
    private static TurnState state;
    public float Rotspeed = 10;
    int layermask = 1 << 9;
    // Use this for initialization
    void Start()
    {
        state = TurnState.Right;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 100, Color.green);

        Debug.DrawLine(this.transform.position, target.transform.position, Color.blue);
        Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.right) * 100, Color.red);
        Debug.Log(state);
        AssignEnemyTurn();
        Changestate();
    }
    /// <summary>
    /// Rotates the enemy to the position of the target
    /// </summary>
    private void AssignEnemyTurn()
    {
        Vector3 enemyToTargetVect = target.transform.position - this.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layermask))
        {
            state = TurnState.Stop;
            Debug.DrawLine(this.transform.position, this.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else if(Vector3.Dot(this.transform.TransformDirection(Vector3.right), enemyToTargetVect) > 0)
        {
            state = TurnState.Right;
        }
        else if (Vector3.Dot(this.transform.TransformDirection(Vector3.right), enemyToTargetVect) < 0)
        {
            state = TurnState.Left;
        }
        else if (Vector3.Dot(this.transform.TransformDirection(Vector3.right), enemyToTargetVect) == 0)
        {
            state = TurnState.Stop;
        }
        
        Debug.Log(Vector3.Dot(this.transform.TransformDirection(Vector3.right), enemyToTargetVect));
    }
    /// <summary>
    /// Changes the Turning state of the enemy
    /// </summary>
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
    }
}
