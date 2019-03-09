using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerRoom_PipesRocks : MonoBehaviour
{
    [SerializeField] private Animator myAnimatorController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("Front_Door", true);
            Destroy(gameObject);

            AkSoundEngine.PostEvent("Play_MetalDoorSlamCloseNoPower", gameObject);
        }
    }
}
