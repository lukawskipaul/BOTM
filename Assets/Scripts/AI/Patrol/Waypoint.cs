using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will be attached to empty Game Objects
// used by the patrol script to create a route for the
// Croc to follow as it patrols.
// 
// This script only draws a red wire sphere around each
// waypoint so that they are easy to see when debugging.
//
// A Game Object MUST have this script attached in order
// to be considered part of the Croc's patrol route.

public class Waypoint : MonoBehaviour
{
    // The size of the sphere around each Waypoint.
    public float SphereDrawRadius = 1.0f;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, SphereDrawRadius);
    }
}
