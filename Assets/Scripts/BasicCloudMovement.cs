using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCloudMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speed, 0, 0);
    }
}
