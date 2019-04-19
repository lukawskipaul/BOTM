using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Requires components on the GameObject that will be used by the script
[RequireComponent(typeof(Rigidbody))]
//Animator will be required if using any Animation
//[RequireComponent(typeof(Animator))]
public class PlayerRespawnScript : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    private CheckpointScript currentCheckpoint;
    private Rigidbody rb;
    [SerializeField]
    private Text checkpointObjectiveText;

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
            SceneManager.LoadScene(sceneToLoad);

            //Things that need to be reset to intial value will go under here
            //
        }
        else
        {
            transform.position = currentCheckpoint.transform.position;
            GetComponent<PlayerHealth>().HealPlayer(100);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCurrentCheckpoint(other.GetComponent<CheckpointScript>());
        }

        // the plan is to include multiple checkpoints with different tags (checkpoint, checkpoint2, checkpoint3, etc). 
        // as the player passes each checkpoint, the objective text in the journal will update
        if (other.CompareTag("Checkpoint"))
        {
            checkpointObjectiveText.text = "This is your new objective";
        }
    }
}
