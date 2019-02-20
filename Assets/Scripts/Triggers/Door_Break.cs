using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Break : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TeleObjects")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
