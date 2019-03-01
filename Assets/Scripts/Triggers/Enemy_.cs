using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ : MonoBehaviour
{
    public MonoBehaviour AgentScript;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AgentScript.enabled = true;
        }
    }
}
