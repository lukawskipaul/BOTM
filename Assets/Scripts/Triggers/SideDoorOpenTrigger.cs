using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoorOpenTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
}
