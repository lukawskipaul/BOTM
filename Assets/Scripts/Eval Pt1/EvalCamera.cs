using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalCamera : MonoBehaviour {
    public Transform target;
    //int layermask = 1 << 9;
    private float targetTurnSpeed,moveCameraDistance = 0.347f;

    private Vector3 distanceFromPlayer;
    
	// Use this for initialization
	void Start () {
        //layermask = ~layermask;
        targetTurnSpeed = target.GetComponent<EvalPlayer>().turnSpeed;
        distanceFromPlayer = this.transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Quaternion camTurnAngle;
        if (target.gameObject.activeSelf)
        {
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.D))
            {
                camTurnAngle = Quaternion.AngleAxis(targetTurnSpeed * Time.deltaTime, Vector3.up);
                distanceFromPlayer = camTurnAngle * distanceFromPlayer;
            }
            if (KeyboardInputUtil.IsHoldingKey(KeyCode.A))
            {
                camTurnAngle = Quaternion.AngleAxis(-1 * targetTurnSpeed * Time.deltaTime, Vector3.up);
                distanceFromPlayer = camTurnAngle * distanceFromPlayer;
            }
        }
        
        this.transform.position = target.transform.position + distanceFromPlayer;
        CameraCollision();
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,target.eulerAngles.y,this.transform.eulerAngles.z);
        
	}
    void CameraCollision()
    {
        Vector3 targetCollisionDistance = target.position + new Vector3(0, target.lossyScale.y / 10, target.lossyScale.z / 10);
        RaycastHit hit;
        Debug.DrawLine(this.transform.position, target.position + new Vector3(0,target.lossyScale.y / 10,target.lossyScale.z / 10), Color.cyan);
        //layermask
        if (Physics.Linecast(this.transform.position, targetCollisionDistance, out hit))
        {
            if (hit.collider.tag != "Player")
            {
                this.transform.position = Vector3.Lerp(this.transform.position, target.position, hit.distance * moveCameraDistance);
            }
            
        }
    }
    
}
