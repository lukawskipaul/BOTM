using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawnScript : MonoBehaviour
{
    private CheckpointScript currentCheckpoint;
    private Rigidbody rb;

    private void SetCurrentCheckpoint(CheckpointScript newCurrentCheckpoint)
    {
        if(currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    private void RespawnPlayer()
    {
        rb.velocity = Vector3.zero;

        //

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
