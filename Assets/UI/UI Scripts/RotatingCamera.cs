using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera rotating around a point.

public class RotatingCamera : MonoBehaviour {

	[Tooltip("Target for the camera to rotate around")]
    public GameObject target;

    [SerializeField]
    private float speedMod = 5f;
    private Vector3 point;

    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);
    }

    void Update()
    {
        transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
    }
}
