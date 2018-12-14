using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : PowerUp
{
    public BasicMove playerMovement;
    public Rigidbody player;
    public float verticalForceApplied = 10f;
    public float forwardForceApplied = 30f;
    public float forceAppliedToRock = 20f;
    private bool shouldPush = false;
    private float timeToWait = 1f;

    public override string PowerName
    {
        get
        {
            return "Push";
        }
    }

    //Check to see if object is Moveable
    //check if player has powerup to push object
    //if both are true, player can move object

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)&& playerMovement.isSprinting && playerMovement.canMove)
        {
            //Press f to pay respects <_<
            playerMovement.canMove = false;
            shouldPush = true;
            player.AddRelativeForce(new Vector3(0f, verticalForceApplied, 0f), ForceMode.Impulse);
            player.AddRelativeForce(new Vector3(0f, 0f, forwardForceApplied), ForceMode.Impulse);
            Debug.Log("YEET YOURSELF");
            StartCoroutine(HoldPlayerPosition());
        }
    }

    IEnumerator HoldPlayerPosition()
    {
        float timeWaited = 0f;
        while(timeWaited < timeToWait)
        {
            timeWaited += Time.deltaTime;
            yield return null;
        }
        playerMovement.canMove = true;
        shouldPush = false;
        yield return null;
    }

    //TODO: make the player be sprinting
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Moveable")
        {
            Debug.Log("Rock hit");
            if (IsActivated && IsUnlocked && shouldPush)
            {
                collision.rigidbody.isKinematic = false;
                Vector3 direction = collision.transform.position - transform.position;
                direction.Normalize();
                collision.rigidbody.AddForce(direction * forceAppliedToRock, ForceMode.Impulse);
                Debug.Log("Should move block");
            }
            else
            {
                if(!collision.rigidbody.isKinematic)
                {
                    collision.rigidbody.isKinematic = true;
                }
            }
        }
        if(collision.transform.tag == "Breakable")
        {
            if (IsActivated && IsUnlocked && shouldPush)
            {
                Destroy(collision.collider.gameObject);
            }
        }
    }

    // reset after pushing is done
    //this will need to be changed so that the object falls normally instead of just stops
    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.tag == "Moveable")
        {
            //collision.rigidbody.isKinematic = true;
        }
    }


}

