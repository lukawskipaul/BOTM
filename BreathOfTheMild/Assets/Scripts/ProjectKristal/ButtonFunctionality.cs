using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour {

    [SerializeField]
    private GameObject mainMenu, creditsMenu, mainMenuLight, creditMenuLight;

    [SerializeField]
    private string startLevel;

    public void OnStartClick()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void OnCreditsClick()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
        mainMenuLight.SetActive(false);
        creditMenuLight.SetActive(true);
    }

    public void OnBackClick()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        mainMenuLight.SetActive(true);
        creditMenuLight.SetActive(false);
    }


    public void OnQuitClick()
    {
        Application.Quit();
    }

	
}
