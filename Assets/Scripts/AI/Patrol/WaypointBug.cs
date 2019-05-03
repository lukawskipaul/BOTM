using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBug : MonoBehaviour
{
    [SerializeField]
    float debugDrawRadius = 1.0F;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}
