using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRailway : TriggerableObject
{
    public Transform[] target;
    public float speed;
    private int current;

    void Update()
    { 
        if (GetComponent<TriggerableObject>().IsTriggered == true)
        {
            Railway();
        }
        if (GetComponent<TriggerableObject>().isTriggered == false)
        {
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
