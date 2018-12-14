using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoMovement : Movement {
    Rigidbody enemyrigid;
    Vector3 axisToRot;
    float pushForce;
    public RhinoMovement(Rigidbody _enemyrigid,float _rotspeed, float _force)
    {
        this.Direction = Vector3.forward;
        enemyrigid = _enemyrigid;
        this.Speed = _rotspeed;
        this.pushForce = _force;
        axisToRot = Vector3.up;
    }
    public void UpdateMovement()
    {
        RotationMovement();
        SightDetection();
    }
    private void RotationMovement()
    {
        if (Rhino.state == RhinoState.Searching)
        {
            if (Random.value >= 5)
            {
                enemyrigid.gameObject.transform.Rotate(axisToRot * this.Speed * Time.deltaTime);
            }
            else
            {
                enemyrigid.gameObject.transform.Rotate(-(axisToRot * this.Speed * Time.deltaTime));
            }
            
        }
    }
    private void SightDetection()
    {
        if (Rhino.state == RhinoState.Charging)
        {
            enemyrigid.AddForce(enemyrigid.gameObject.transform.TransformDirection(this.Direction * pushForce));
            Rhino.state = RhinoState.Searching;
        }
    }
}
