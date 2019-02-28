using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVoiceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            

            AkSoundEngine.PostEvent("", gameObject); //put in the first \ai voiceover to play at the entrance
        }
    }
}
