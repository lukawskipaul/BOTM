using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainBtn : MonoBehaviour
{
    [SerializeField]
    private string MainMenuScene;

    public void QuitToMainButton()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
