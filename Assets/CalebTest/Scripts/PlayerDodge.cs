using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerDodge : MonoBehaviour
{
    [SerializeField]
    private float dodgeDistance = 100.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Dodge()
    {
        if (Input.GetButtonDown("Dodge") /*&& isOnGround*/)
        {
            rb.AddForce(transform.forward * dodgeDistance, ForceMode.Impulse);
        }

        //isDodging = true;

        //Implement dodge timer
        //Implement I-frames counter
        //Add Dodge Anim
    }
}
