using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRailway : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    private int current;
    TriggerableObject Triggerable;

    void Update()
    { 
        if (GetComponent<TriggerableObject>().isTriggered == true)
        {
            Railway();
        }
       
    }
    public void Railway()
    {
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % target.Length;
    }
    
}
