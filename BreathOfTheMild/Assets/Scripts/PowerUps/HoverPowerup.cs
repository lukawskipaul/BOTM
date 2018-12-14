using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPowerup : PowerUp {

    [SerializeField]
    GameObject player;
    [SerializeField]
    float levitateHeight = 1f;

    Rigidbody playerRigidbody;
    bool isHovering = false;

	// Use this for initialization
	void Start () {
        playerRigidbody = player.GetComponent<Rigidbody>();
	}
	

    public override void UsePower()
    {
        if (isHovering)
        {
            playerRigidbody.useGravity = true;
            isHovering = false;
        }
        else if (!isHovering)
        {
            playerRigidbody.useGravity = false;
            Vector3 playerTransfrom = playerRigidbody.transform.position;
            playerTransfrom.y += levitateHeight;
            player.transform.position = playerTransfrom;
            isHovering = true;
        }
        

    }
}
