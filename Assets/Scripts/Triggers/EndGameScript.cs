using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    [HideInInspector]
    public static bool hasBeenPickedUp = false;

    [SerializeField]
    private GameObject PickupPromptText;
    [SerializeField]
    private GameObject toBeContinuedCanvas;
    [SerializeField]
    private RootMotionMovementController movement;
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private Image ScreenFade;
    [SerializeField]
    private SlowMoGameTime slowMo;
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private float fadeInRate;

    private Color fadeInAlpha;

    private bool isInTrigger;
    private bool hasBeenCalled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !hasBeenPickedUp)
        {
            isInTrigger = true;
            PickupPromptText.SetActive(true);
            //Debug.Log("Player entered pickup zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !hasBeenPickedUp)
        {
            isInTrigger = false;
            PickupPromptText.SetActive(false);
            //Debug.Log("Player left pickup zone");
        }
    }

    private void Update()
    {
        if(isInTrigger && !hasBeenCalled)
        {
            if(Input.GetButtonDown("Interact"))
            {
                hasBeenCalled = true;
                hasBeenPickedUp = true;
                movement.DisableMovement(4f);
                PlayerAnimator.SetTrigger("TakeDamage");
                toBeContinuedCanvas.SetActive(true);
                slowMo.SlowMo();
                StartCoroutine(WaitForFade());
                StartCoroutine(FadeToBlack());
            }
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator FadeToBlack()
    {
        while(ScreenFade.color.a < 256)
        {
            fadeInAlpha.a = ScreenFade.color.a;
            fadeInAlpha.a += Time.deltaTime * fadeInRate;
            ScreenFade.color = fadeInAlpha;
            yield return null;
        }
    }

}
