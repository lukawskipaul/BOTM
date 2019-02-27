using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Break : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "TeleObjects" || other.gameObject.tag == "ThrownObj")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
