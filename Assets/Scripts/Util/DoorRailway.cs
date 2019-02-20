using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRailway : TriggerableObject
{
    public Transform[] target;
    public Transform[] target2;
    public float speed;
    private int current;

    void Update()
    { 
        if (GetComponent<TriggerableObject>().IsTriggered == true)
        {
            RailwayOpen();
        }
        if (GetComponent<TriggerableObject>().isTriggered == false)
        {
            RailwayClose();
        }
       
    }
    public void RailwayOpen()
    {
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % target.Length;
    }
    public void RailwayClose()
    {
        if (transform.position != target2[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target2[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % target2.Length;
    }

}
