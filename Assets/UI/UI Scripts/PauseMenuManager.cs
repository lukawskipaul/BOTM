using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject journalMenu;
    public GameObject pauseMenu;
    public GameObject controlsCanvas;

    public string journalInput;
    public string pauseInput;
    public string controlsInput;

    public bool paused;

    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        journalMenu.gameObject.SetActive(false);
        controlsCanvas.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(pauseInput))
        {
            if(paused == false)
            {
                PauseGame();
            }

            else if(paused == true)
            {
                ClosePauseMenu();
            }
        }

        if (Input.GetButton(journalInput))
        {
            if (paused == false)
            {
                OpenJournal();
            }

            else if (paused == true)
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
        paused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void CloseJournal()
    {
        Time.timeScale = 1;
        journalMenu.gameObject.SetActive(false);
        paused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
