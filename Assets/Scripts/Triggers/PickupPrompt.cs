using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupPrompt : MonoBehaviour
{
    [HideInInspector]
    public bool hasBeenPickedUp = false;
    [SerializeField]
    private GameObject ObjectToGivePlayer;  //if any
    [SerializeField]
    private GameObject PickupPromptText;
    [SerializeField]
    private GameObject PickUpObjectCanvas;
    private bool isInTrigger;
    private bool paused;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            isInTrigger = true;
            PickupPromptText.SetActive(true);
            //Debug.Log("Player entered pickup zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pickup")
        {
            isInTrigger = false;
            PickupPromptText.SetActive(false);
            //Debug.Log("Player left pickup zone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (ObjectToGivePlayer != null)
                ObjectToGivePlayer.SetActive(true);
            hasBeenPickedUp = true;
            PickupPromptText.SetActive(false);
            isInTrigger = false;
            Destroy(this.gameObject);
            PickUpObjectCanvas.SetActive(true);
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //pauseMenu.gameObject.SetActive(true);
        paused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
