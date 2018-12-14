using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int sceneNumber = 0;

    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneNumber);
    }

}