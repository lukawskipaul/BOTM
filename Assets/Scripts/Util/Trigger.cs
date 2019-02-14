﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    TriggerableObject Triggerable;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Triggerable.IsTriggered = true;
        }
    }
}
