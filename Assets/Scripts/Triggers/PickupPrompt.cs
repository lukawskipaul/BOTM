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
    private PauseMenuManager pauseMenuManager;
    [SerializeField]
    private GameObject ObjectToGivePlayer;  //if any
    [SerializeField]
    private GameObject PickupPromptText;
    [SerializeField]
    private bool isJournalPickup;
    private bool isInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenPickedUp)
        {
            isInTrigger = true;
            PickupPromptText.SetActive(true);
            //Debug.Log("Player entered pickup zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !hasBeenPickedUp)
        {
            isInTrigger = false;
            PickupPromptText.SetActive(false);
            //Debug.Log("Player left pickup zone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && !hasBeenPickedUp)
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (ObjectToGivePlayer != null && !isJournalPickup)
                ObjectToGivePlayer.SetActive(true);
            hasBeenPickedUp = true;
            AkSoundEngine.PostEvent("Play_TK_PickUp", gameObject);
            PickupPromptText.SetActive(false);
            isInTrigger = false;
            if (isJournalPickup)
            {
                pauseMenuManager.pickupObjectCanvas = ObjectToGivePlayer;
                pauseMenuManager.JournalPiecePickup();
            }
            Destroy(this.gameObject);
        }
    }
}
