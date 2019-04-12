using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(SphereCollider))]

//This script goes on a sphere collider near the player's feet childed to the player GameObject
public class GroundCheck : MonoBehaviour
{
    private RootMotionMovementController movement;

    private void Start()
    {
        movement = this.gameObject.GetComponentInParent<RootMotionMovementController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        /* Check if player is on a walkable surface */
        if (other.gameObject.tag == "Ground")       //need to use ground tag for any walkable surface
        {
            movement.IsOnGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        /* Check if player is not on a walkable surface */
        if (other.gameObject.tag == "Ground")
        {
            movement.IsOnGround = false;
        }
    }
}
