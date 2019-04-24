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
    private static int JournalInstances;//# of journals in the game
    private int curJournalID;//The ID # for this journal
    private void Awake()
    {
        //Deletes all save data from previous session
        PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        JournalInstances++;//add to current number of journals in the game
        curJournalID = JournalInstances;//Set the ID # for this Journal instance
    }
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
        GameResetSaveInfo();
        if (isInTrigger && !hasBeenPickedUp)
        {
            CheckInput();
        }
        
        
    }
    /// <summary>
    /// When the game resets(or player dies), if the journal has been picked up already DONT SHOW IT AGAIN! 
    /// </summary>
    private void GameResetSaveInfo()
    {
        if (PlayerPrefs.GetInt("JournalID" + curJournalID) == 1)
        {
            Destroy(this.gameObject);
        }
    }
    private void CheckInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (ObjectToGivePlayer != null && !isJournalPickup)
                ObjectToGivePlayer.SetActive(true);
            hasBeenPickedUp = true;
            PlayerPrefs.SetInt("JournalID"+curJournalID,1);//Save the data that this journal has been picked up already
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
