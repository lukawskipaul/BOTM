using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour {

    Rigidbody rigidbody;

    [SerializeField]
    float distance = 2f;

    [SerializeField]
    LayerMask groundLayers;

    bool isInAir = false;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        IsAirborne();
        SetKinematic();
        Debug.Log("Is in air: " + isInAir);
	}

    void IsAirborne()
    {
        Vector3 testDir = new Vector3(0, -distance);
        //Debug.DrawRay(transform.position, testDir, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, distance, groundLayers))
        {
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }
    }

    void SetKinematic()
    {
        if (isInAir)
        {
            rigidbody.isKinematic = false;
        }
        else if (!isInAir)
        {
            rigidbody.isKinematic = true;
        }
    }
}
