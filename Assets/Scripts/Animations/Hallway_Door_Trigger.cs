using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway_Door_Trigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimatorController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("Front_Door", true);
            Destroy(gameObject);
            
        }
    }



}








