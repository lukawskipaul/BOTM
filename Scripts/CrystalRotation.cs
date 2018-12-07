using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotation : MonoBehaviour {

    public float RotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(Vector3.down, (RotationSpeed * Time.deltaTime));
	}
}
