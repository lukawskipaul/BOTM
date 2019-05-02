using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad;

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
