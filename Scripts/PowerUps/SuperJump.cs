using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : PowerUp {

    [SerializeField]
    BasicMove playerBasicMove;

    public override string PowerName
    {
        get
        {
            return "Super Jump";
        }
    }

    private void FixedUpdate()
    {
        if (this.IsActivated)
        {
            Jump();
        }
        
    }

    private void Jump()
    {
        playerBasicMove.SuperJump();
    }
}
