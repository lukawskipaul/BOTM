using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushback : MonoBehaviour {

    [SerializeField]
    private float pushbackForce;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player"/*other.CompareTag("Player")*/)
        {
            Vector3 pushbackDirection = -(other.transform.position - transform.position);

            pushbackDirection =- pushbackDirection.normalized;

            other.GetComponent<Rigidbody>().AddForce(pushbackDirection * pushbackForce * 100);
            Debug.Log("Push hit" + pushbackDirection);
        }        
    }
}
