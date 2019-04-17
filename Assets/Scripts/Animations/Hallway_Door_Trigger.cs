using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_Door_Trigger : MonoBehaviour
{
    [SerializeField] private SlowMoGameTime SlowMo;
    [SerializeField] private Animator myAnimatorController;
    [SerializeField] private Animator playerController;
    [SerializeField] private GameObject playerLight;
    [SerializeField] private GameObject dustClouds;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("Front_Door", true);
            playerController.SetTrigger("TakeDamage");
            dustClouds.SetActive(true);
            SlowMo.SlowMo();
            //playerLight.SetActive(true);

            AkSoundEngine.PostEvent("Play_MetalDoorSlamCloseNoPower", gameObject);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }



}








