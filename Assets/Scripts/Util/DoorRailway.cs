using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRailway : TriggerableObject
{
    public Transform[] OpenPoint;
    public Transform[] ClosePoint;
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
        if (transform.position != OpenPoint[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, OpenPoint[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % OpenPoint.Length;
    }
    public void RailwayClose()
    {
        if (transform.position != ClosePoint[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, ClosePoint[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else current = (current + 1) % ClosePoint.Length;
    }

}
