using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The object that is being used as a Checkpoint will need a BoxCollider attached to it
[RequireComponent(typeof(BoxCollider))]
//!!!!!IMPORTANT!!!!!
//I'm not sure how to require an Is Trigger as true, but it has to be a Trigger
public class CheckpointScript : MonoBehaviour
{
    private bool isActivated;

    public void SetIsActivated(bool value)
    {
        Debug.Log(gameObject.name + "is Activated");
        isActivated = value;
        GetComponent<BoxCollider>().enabled = false;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            SetIsActivated(true);
    }
    */
}
