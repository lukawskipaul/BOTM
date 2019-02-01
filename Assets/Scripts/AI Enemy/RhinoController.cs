using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoController : MonoBehaviour {
    public Rigidbody playerRigidbody;
    private Rigidbody enemyRigidbody;
    private float RotYSpeed;
    private float ChargeForce;
    public Vector3 PlayerBoop = new Vector3(0,400,100);
    Rhino rhino;
	// Use this for initialization
	void Start () {
        RotYSpeed = Random.Range(10, 40);
        ChargeForce = Random.Range(500, 700);  //changed for demo by Brendan. Original values (25,60)
        enemyRigidbody = GetComponent<Rigidbody>();
        playerRigidbody = playerRigidbody.GetComponent<Rigidbody>();

        rhino = new Rhino( enemyRigidbody, RotYSpeed,ChargeForce);
        rhino.Start();
	}
	
	// Update is called once per frame
	void Update () {
        rhino.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerRigidbody.gameObject)
        {
            playerRigidbody.AddForceAtPosition(new Vector3(0,500,200), playerRigidbody.gameObject.transform.position);
            Debug.Log(playerRigidbody);
        }
        
    }

}
