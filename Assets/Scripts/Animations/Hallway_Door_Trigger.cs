using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_Door_Trigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimatorController;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject directionalLight;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("Front_Door", true);
            playerLight.SetActive(true);
            directionalLight.SetActive(false);

            AkSoundEngine.PostEvent("Play_MetalDoorSlamCloseNoPower", gameObject);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }



}








