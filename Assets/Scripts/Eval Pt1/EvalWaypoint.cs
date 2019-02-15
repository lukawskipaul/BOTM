using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody))]
public class EvalWaypoint : MonoBehaviour {
    public GameObject enemyTank;
    private EvalEnemy enemyScript;
	// Use this for initialization
	void Start () {
        enemyScript = enemyTank.GetComponent<EvalEnemy>();
        this.GetComponent<Rigidbody>().useGravity = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall Touched! Relocating...");
            enemyScript.MoveToNextRandomWayPoint();
        }
    }
}
