using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDashPower : PowerUp {

    [SerializeField]
    Rigidbody playerRigidBody;

    [SerializeField]
    float dashStrength = 5f;

    [SerializeField]
    float cooldown = 5f;

    [SerializeField]
    BasicMove basicMove;

    private bool canDash = true;

    public override string PowerName
    {
        get
        {
            return "Air Dash";
        }
    }

    public override void UsePower()
    {
        AirDash();
    }

    private void AirDash()
    {
        //playerRigidBody.useGravity = false;
        if (canDash && !basicMove.isOnGround)
        {
            Debug.Log(basicMove.isOnGround);
            playerRigidBody.AddForce(playerRigidBody.transform.forward * dashStrength);
            canDash = false;
            StartCoroutine(CoolDown());
        }        
        
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(cooldown);
        canDash = true;
    }
}
