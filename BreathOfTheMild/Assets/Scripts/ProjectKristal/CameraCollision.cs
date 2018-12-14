using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    [SerializeField]
    private float minDistance = .5f; //minimum distance to the player from camera
    [SerializeField]
    private float maxDistance = 5f; //maximum distance to the player from the camera
    [SerializeField]
    private float smooth = 5f; //how quickly camera moves towards the player after hitting object
    [SerializeField]
    private float hitPercent = .8f; // 
    [SerializeField]
    LayerMask triggerLayer;

    private Vector3 cameraDir; //where the camera is moving to (relative to player)
    private Vector3 desiredCameraPosition; //where the camera should be (relative to player)
    private float distance; //distance away from player at start

    private void Awake()
    {
        cameraDir = transform.localPosition.normalized; //converting world units to local units (for parent object)
        distance = transform.localPosition.magnitude; //converting world units to local units (for parent object)
        triggerLayer = ~triggerLayer;
    }

    private void Update()
    {
        desiredCameraPosition = player.TransformPoint(cameraDir * maxDistance);

        CheckHit(); // see if object is between camera and player
        transform.localPosition = Vector3.Lerp(transform.localPosition,
            cameraDir * distance, Time.deltaTime * smooth);
        // move camera to player regardless of whether or not an object is between player or not
        // move camera in local units since it is a child of the player game object
    }

    private void CheckHit()
    {
        RaycastHit hit;
        // if camera hits object between it and player, camera goes as close as it can to player
        // relative between its max distance and its minimum distance
        if (Physics.Linecast(player.position, desiredCameraPosition, out hit, triggerLayer))
        {
            distance = Mathf.Clamp((hit.distance * hitPercent), minDistance, maxDistance);
        }
        // otherwise, camera stays at its max distance from player
        else
        {
            distance = maxDistance;
        }
    }
}

