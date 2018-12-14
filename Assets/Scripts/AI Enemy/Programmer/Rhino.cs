using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RhinoState {Searching, Charging}
public class Rhino : Enemy {
    public static RhinoState state;
    RhinoMovement movement;
    int LayerMask = 1 << 9;

    public Rhino(Rigidbody _enemyrigid,float _rotspeed,float _chargeforce):base(_enemyrigid)
    {
        movement = new RhinoMovement(_enemyrigid,_rotspeed,_chargeforce);
        state = RhinoState.Searching;
        enemyRigid.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public override void Start()
    {
        
    }
    public void Update()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(this.enemyRigid.transform.position, this.enemyRigid.transform.TransformDirection(movement.Direction), out hit, Mathf.Infinity, LayerMask))
        {
            state = RhinoState.Charging;
        }
        movement.UpdateMovement();
        Debug.Log("CurrentState: "+state);
    }
    
    
}
