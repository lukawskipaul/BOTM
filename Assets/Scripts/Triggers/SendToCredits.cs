using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToCredits : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuCanvas;
    [SerializeField]
    private GameObject CreditsCanvas;
    private void OnEnable()
    {
        if(EndGameScript.hasBeenPickedUp)
        {
            MainMenuCanvas.SetActive(false);
            CreditsCanvas.SetActive(true);
            EndGameScript.hasBeenPickedUp = false;
        }
    }
}
