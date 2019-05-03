using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    [SerializeField]
    private CrystalCollectibleText CCT;
    [SerializeField]
    private GameObject PickupPromptText;
    private bool isInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        CCT.AddInGameCrystal();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInTrigger = true;
            PickupPromptText.SetActive(true);
            //Debug.Log("Player entered pickup zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInTrigger = false;
            PickupPromptText.SetActive(false);
            //Debug.Log("Player left pickup zone");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.GetButtonDown("Interact" )&& isInTrigger)
        {
            CCT.GainedCrystal();
            AkSoundEngine.PostEvent("Play_UI_PageFlip", gameObject);
            Destroy(this.gameObject);
        }
    }
}
