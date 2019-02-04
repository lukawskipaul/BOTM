using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMover : MonoBehaviour
{
    public Rail rail;
    public Transform lookAt;
    public bool smoothMove = true;
    public float moveSpeed = 5.0f;

    private Transform thisTransform;

    private Vector3 lastPosition;






    private void Start()
    {
        thisTransform = transform;
        lastPosition = thisTransform.position;

    }


    private void Update()
    {


        if (smoothMove)
        {
            lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(lookAt.position), Time.deltaTime);
            thisTransform.position = lastPosition;

        }
        else
        {
            thisTransform.position = rail.ProjectPositionOnRail(lookAt.position);

        }



        thisTransform.LookAt(lookAt.position);

    }
}
