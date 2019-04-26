using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//GameObjects with this script require the components below, a component will be added if one does not exist
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

//This script goes on the player
public class PlayerRespawnScript : MonoBehaviour
{
    #region Variables

    public GameObject EnemyCroc;
    public ScreenFade screenFade;
    private CheckpointScript currentCheckpoint;
    private Rigidbody rb;
    private Animator anim;

    private const string deathBoolName = "isDying";

    #endregion

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            SetCurrentCheckpoint(other.GetComponent<CheckpointScript>());
        }
    }

    #region Animation Events

    /* Remember, changing name of animation event functions requires changing the function in the animation event! */

    /* Called during specific death animation frame to activate player respawn */
    public void RespawnPlayer()
    {
        anim.SetBool(deathBoolName, false);

        rb.velocity = Vector3.zero;

        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Things that need to be reset to intial value will go under here
            //
        }
        else
        {
            screenFade.BeginFade(1);
            StopCoroutine(FadeOut());
            StartCoroutine(FadeOut());
            transform.position = currentCheckpoint.transform.position;
            //GetComponent<PlayerHealth>().HealPlayer(100);
            this.GetComponent<PlayerHealth>().CurrentHealth = 100;

            //anim.SetTrigger("Respawn");
            //Reset Death animation
            anim.ResetTrigger("Death");
            //Reset Telekinesis
            anim.ResetTrigger("TKPull");
            anim.SetBool("isUsingTelekinesis", false);
            anim.SetBool("isDoingTKThrow", false);
            //Reset Stagger animation
            anim.ResetTrigger("TakeDamage");
            //Reset Movement
            anim.Play("Movement");
            //anim.SetTrigger("Respawn");
            //If croc was already dead... stay dead!
            if (PlayerPrefs.GetInt("CrocDead") == 1)
            {
                //Deactivates croc
                EnemyCroc.SetActive(false);
            }
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(3f);
        screenFade.BeginFade(-1);
    }

    #endregion
}
