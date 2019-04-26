using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneLoaderv2 : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingText;
    [SerializeField]
    private GameObject continueText;
    [SerializeField]
    private string sceneToLoad;
    
    private AsyncOperation asyncOperation;
    private bool isReady = false;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if(isReady && asyncOperation != null)
        {
            if(Input.anyKeyDown)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                isReady = true;
                continueText.SetActive(true);
                loadingText.SetActive(false);
            }
            yield return null;
        }
    }
}
