
using Assets.SpidaLib.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTargetTrack : MonoBehaviour {
    public GameObject enemy;
    int layermask = 1 << 9;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Debug.DrawLine(this.transform.position,this.transform.position + (KineticStrafingController.playerdir.normalized * 100),Color.green);
        if (Physics.Raycast(this.transform.position,this.transform.TransformDirection(KineticStrafingController.playerdir.normalized),out hit,Mathf.Infinity,layermask))
        {
            Debug.DrawLine(this.transform.position, this.transform.position + (KineticStrafingController.playerdir.normalized * hit.distance), Color.yellow);
            Debug.Log(hit.distance);
        }
        Debug.DrawLine(this.transform.position, enemy.transform.position,Color.magenta);
        Debug.Log("Degrees to see target: " + CalculateRotationToEnemy());
	}
    private float CalculateRotationToEnemy()
    {
        Vector3 playerToEnemyVect = enemy.transform.position - this.transform.position;
        Vector3 playerSight = this.transform.TransformDirection(Vector3.forward);
        return VectorUtil.VectorAngle(playerToEnemyVect, playerSight);
    }
}
