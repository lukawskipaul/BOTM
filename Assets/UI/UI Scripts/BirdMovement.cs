using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

    public float yaxis = 0.1f;
    public float xaxis = 0.1f;
    public float yfrequency = 1f;
    public float xfrequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {

        tempPos = posOffset;

        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * yfrequency) * yaxis;
        tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * xfrequency) * xaxis;

        transform.position = tempPos;
    }
}
