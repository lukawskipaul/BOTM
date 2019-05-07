using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    public void callEvent (string s)
    {
        AkSoundEngine.PostEvent(s, gameObject);
        //Debug.Log("PrintEvent: " + s + " Called at: " + Time.time);
    }

}
