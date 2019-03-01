using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Break : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected");
        if (other.gameObject.tag == "ThrownObj")
        {
            Debug.Log("Thrown object detected");
            GameObject.Destroy(gameObject);
        }
    }
}
