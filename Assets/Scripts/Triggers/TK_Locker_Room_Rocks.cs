using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TK_Locker_Room_Rocks : MonoBehaviour
{
    //[SerializeField] private Animator myAnimatorController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //myAnimatorController.SetBool("Locker_Room_Falling_Stuff", true);
            //Destroy(gameObject);

            //AkSoundEngine.PostEvent("Play_MetalDoorSlamCloseNoPower", gameObject);
        }
    }
}
