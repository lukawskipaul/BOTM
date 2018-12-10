using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    string MainSceneToLoad;
    [SerializeField]
    string CreditsSceneToLoad;
    [SerializeField]
    int startGameDelay = 5;

    [SerializeField]
    Button returnButton;

    //use when we have a loading scene
    //public void PlayButtonClicked()
    //{
    //    MenuMusic music = GameObject.Find("Menu Music").GetComponent<MenuMusic>();
    //    music.StopMusic();

    //    LoadingScene.LoadNewScene(sceneToLoad);
    //}

    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            returnButton.onClick.Invoke();
        }
    }

    public void StartButtonClicked()
    {
        StartCoroutine(StartGameDelay());
    }

    public void CreditsButtonClicked()
    {
        SceneManager.LoadScene(CreditsSceneToLoad);
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
