using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCanvas;

    private static bool isPaused = false;

    private GameObject activeEnemies;

    const string startButtonName = "Start";

    void Start()
    {
        Unpause();
    }

    void Update()
    {
        activeEnemies = GameObject.FindGameObjectWithTag("Enemy");

        if (Input.GetButtonDown(startButtonName) && activeEnemies != null)
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {        
        isPaused = true;
        menuCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    void Unpause()
    {
        isPaused = false;
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ResumeButtonClicked()
    {
        Unpause();
    }

    public void MainMenuButtonClicked()
    {
        Unpause();

        //LoadingScene.LoadNewScene("MainMenu");
        SceneManager.LoadScene("NewMainMenu");
    }
}
