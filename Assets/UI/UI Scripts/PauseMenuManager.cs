﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject journalMenu;
    public GameObject pauseMenu;
    public GameObject controlsCanvas;
    public GameObject pickupObjectCanvas;

    [HideInInspector]
    public bool hasBeenPickedUp = false;
    [SerializeField]
    private GameObject ObjectToGivePlayer;
    [SerializeField]
    private GameObject PickupPromptText;

    private bool isInTrigger;

    [SerializeField]
    //private PickupPrompt thisPickup;

    public string MainMenuScene;
    public string DemoScene;
    public string journalInput;
    public string pauseInput;
    public string controlsInput;

    public bool paused = false;
    public bool journalOpen = false;
    public bool pieceOpen = false;

    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        journalMenu.gameObject.SetActive(false);
        pickupObjectCanvas.gameObject.SetActive(false);
        controlsCanvas.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (isInTrigger)
        {
            CheckInput();
        }

        if (Input.GetButtonDown(pauseInput))
        {
            if(paused == false && journalOpen == false && pieceOpen == false)
            {
                PauseGame();
            }

            else if(paused == true)
            {
                ClosePauseMenu();
            }

            else if(journalOpen == true)
            {
                CloseJournal();
            }

            else if(pieceOpen == true)
            {
                CloseJournalPiece();
            }
        }

        if (Input.GetButtonDown(journalInput))
        {
            if (paused == false && journalOpen == false && pieceOpen == false)
            {
                OpenJournal();
            }

            else if (journalOpen == true)
            {
                CloseJournal();
            }
        }

        if (Input.GetButton(controlsInput))
        {
            controlsCanvas.gameObject.SetActive(true);
        }
        else
        {
            controlsCanvas.gameObject.SetActive(false);
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        paused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        paused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void OpenJournal()
    {
        Time.timeScale = 0;
        journalMenu.gameObject.SetActive(true);
        journalOpen = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AkSoundEngine.PostEvent("Play_UI_JournalOpen", gameObject);
    }
    private void CloseJournal()
    {
        Time.timeScale = 1;
        journalMenu.gameObject.SetActive(false);
        journalOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AkSoundEngine.PostEvent("Play_UI_JournalClose", gameObject);
    }


    private void JournalPiecePickup()
    {
        Time.timeScale = 0;
        pickupObjectCanvas.gameObject.SetActive(true);
        pieceOpen = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseJournalPiece()
    {
        Time.timeScale = 1;
        pickupObjectCanvas.gameObject.SetActive(false);
        pieceOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void QuitToMainButton()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(MainMenuScene);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(DemoScene);
    }

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

    private void CheckInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (ObjectToGivePlayer != null)
                ObjectToGivePlayer.SetActive(true);
            hasBeenPickedUp = true;
            PickupPromptText.SetActive(false);
            isInTrigger = false;
            ObjectToGivePlayer.gameObject.SetActive(false);
            JournalPiecePickup();
        }
    }

    //Texture for the screen to fade from.
    public Texture2D fadeOutTexture;
    [Tooltip("Speed of the screen fade. Lower is Slower")]
    public float fadeSpeed = 1.0f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDirection = -1;

    private void OnGUI()
    {
        alpha += fadeDirection * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDirection = direction;
        return (fadeSpeed);
    }

    private void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }

}
