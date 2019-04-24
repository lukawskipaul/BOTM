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
    public GameObject EnemyCroc;
    private CheckpointScript currentCheckpoint;
    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>();
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
        anim.ResetTrigger("TriggerName");

        //StartCoroutine(RespawnDelay());
        anim.SetBool("isDying", true);

        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Things that need to be reset to intial value will go under here
            //
        }
        else
        {
            transform.position = currentCheckpoint.transform.position;
            //GetComponent<PlayerHealth>().HealPlayer(100);
            this.GetComponent<PlayerHealth>().CurrentHealth = 100;

            //anim.SetTrigger("Respawn");

            //If croc was already dead... stay dead!
            if (PlayerPrefs.GetInt("CrocDead") == 1)
            {
                //Deactivates croc
                EnemyCroc.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCurrentCheckpoint(other.GetComponent<CheckpointScript>());
        }
    }

    private IEnumerator RespawnDelay()
    {
        anim.SetTrigger("Death");

        yield return new WaitForSecondsRealtime(1.0f);

        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Things that need to be reset to intial value will go under here
            //
        }
        else
        {
            transform.position = currentCheckpoint.transform.position;
            GetComponent<PlayerHealth>().HealPlayer(100);
            anim.SetTrigger("Respawn");

            //If croc was already dead... stay dead!
            if (PlayerPrefs.GetInt("CrocDead") == 1)
            {
                //Deactivates croc
                EnemyCroc.SetActive(false);
            }
        }
    }
}
