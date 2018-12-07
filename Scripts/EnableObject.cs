using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour {

    [SerializeField]
    private GameObject objectToEnable;
    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            objectToEnable.SetActive(true);
        }
        
    }
}
