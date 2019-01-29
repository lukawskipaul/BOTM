using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    string MainSceneToLoad;
    [SerializeField]
    string TitleMenuSceneToLoad;
    [SerializeField]
    string CreditsSceneToLoad;
    [SerializeField]
    string ControlsSceneToLoad;
    [SerializeField]
    string OptionsSceneToLoad;
    [SerializeField]
    int startGameDelay = 5;
    


    public void StartButtonClicked()
    {
        StartCoroutine(StartGameDelay());
    }

    public void TitleMenuButtonClicked()
    {
        SceneManager.LoadScene(TitleMenuSceneToLoad);
    }

    public void CreditsButtonClicked()
    {
        SceneManager.LoadScene(CreditsSceneToLoad);
    }

    public void ControlsButtonClicked()
    {
        SceneManager.LoadScene(ControlsSceneToLoad);
    }

    public void OptionsButtonClicjed()
    {
        SceneManager.LoadScene(OptionsSceneToLoad);
    }

    public void ExitGameButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator StartGameDelay()
    {
        yield return new WaitForSeconds(startGameDelay);
        SceneManager.LoadScene(MainSceneToLoad);
    }
}
