using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Requires components on the GameObject that will be used by the script
[RequireComponent(typeof(Rigidbody))]
//Animator will be required if using any Animation
//[RequireComponent(typeof(Animator))]
public class PlayerRespawnScript : MonoBehaviour
{
    private CheckpointScript currentCheckpoint;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void SetCurrentCheckpoint(CheckpointScript newCurrentCheckpoint)
    {
        if(currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);

        Debug.Log("Set Checkpoint");
    }

    //This can be called at the end of a Player Death Animation, for now it's public for the DeathSphereOfDeath script
    public void RespawnPlayer()
    {
        rb.velocity = Vector3.zero;

        //Reset any animation triggers that will be implemented
        //anim.ResetTrigger("TriggerName")

        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Things that need to be reset to intial value will go under here
            //
        }
        else
        {
            transform.position = currentCheckpoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCurrentCheckpoint(other.GetComponent<CheckpointScript>());
        }
    }
}
