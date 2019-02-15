using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas journalMenu;
    public Canvas pauseMenu;

    public bool paused;

    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
        journalMenu.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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

        if (Input.GetButton("Journal"))
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
        
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        paused = true;
    }
    private void ClosePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        paused = false;
    }

    private void OpenJournal()
    {
        Time.timeScale = 0;
        journalMenu.gameObject.SetActive(true);
        paused = true;
    }
    private void CloseJournal()
    {
        Time.timeScale = 1;
        journalMenu.gameObject.SetActive(false);
        paused = false;
    }
}
