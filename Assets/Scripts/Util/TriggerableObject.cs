using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : MonoBehaviour, ITriggerable
{
   public bool isTriggered = false;

    public bool IsTriggered
    {
        get
        {
            return isTriggered;
        }

        set
        {
            isTriggered = value;
        }
    }
}
