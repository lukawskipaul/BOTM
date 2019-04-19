using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenLoader : MonoBehaviour
{
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private Image loadingImage;
    [SerializeField]
    private Text continueText;
    [SerializeField]
    private float minAlpha= .18f;
    [SerializeField]
    private float alphaScaleValue = .8f;
    [SerializeField]
    private string sceneToLoad = "VilcaneLabs";

    private bool shouldIncrease = true;
    private Color continueTextColor;
    private AsyncOperation asyncOperation;

    void Start()
    {
        continueTextColor = continueText.color;
        StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (asyncOperation != null)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        while(!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (loadingImage.enabled == true || loadingText.enabled == true)
                {
                    loadingImage.enabled = false;
                    loadingText.enabled = false;
                }

                if (shouldIncrease)
                {
                    continueTextColor.a += Time.deltaTime*alphaScaleValue;
                    if (continueTextColor.a >= 1)
                        shouldIncrease = false;
                }
                else
                {
                    continueTextColor.a -=  Time.deltaTime*alphaScaleValue;
                    if(continueTextColor.a <= minAlpha)                   
                        shouldIncrease = true;                    
                }
                continueText.color = continueTextColor;
            }
            else
            {
                loadingImage.fillAmount = asyncOperation.progress;
                Debug.Log(loadingImage.fillAmount);
            }
            yield return null;
        }
    }
}
