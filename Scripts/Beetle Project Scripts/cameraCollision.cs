using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCollision : MonoBehaviour
{

    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10f;
    private Vector3 dollyDir;
    private Vector3 dollyDirAdjusted;
    public float distance;


	// Use this for initialization
	void Start ()
	{
	    dollyDir = transform.localPosition.normalized;
	    distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    Vector3 desiredCamPos = transform.parent.TransformPoint(dollyDir * maxDistance);
	    RaycastHit hit;

	    if (Physics.Linecast(transform.parent.position, desiredCamPos, out hit))
	    {
	        distance = Mathf.Clamp((hit.distance * 0.9f), minDistance, maxDistance);

	        } else
	        {
	             distance = maxDistance;
	            
	        }

	    transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);

	}
	}

