using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    GameObject[] leverOrder;

    [HideInInspector]
    public bool correctLever = false;

    [SerializeField]
    Light[] torchLights;

    [SerializeField]
    GameObject creature;

    int i = 0;
    public string[] FirstSentences;
    public string[] SecondSentences;

    private DialogueSystem dialogue;

    public void Start()
    {
        dialogue = FindObjectOfType<DialogueSystem>();
    }

    public void CheckLever(GameObject pressedLever)
    {
        GameObject currentLever = pressedLever;

        if (currentLever == leverOrder[i])
        {
            Debug.Log("Correct lever pulled");
            i++;
            correctLever = true;
            if(i == 1)
            {
                torchLights[0].enabled = true;
                
                
            }
            else if(i == 2)
            {
                torchLights[1].enabled = true;
                torchLights[2].enabled = true;
            }
            else if(i == 3)
            {
                torchLights[3].enabled = true;
                torchLights[4].enabled = true;
                torchLights[5].enabled = true;
            }
            if (i == leverOrder.Length)
            {
                //What we want to happen when the puzzle is solved goes here
                Debug.Log("Correct Order!");
                torchLights[6].enabled = true;
                dialogue.dialogueLines = SecondSentences;
                dialogue.ItemInteraction();
                creature.SetActive(false);
                PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.pushBlock);
            }
        }
        else
        {
            i = 0;
            correctLever = false;
            for(int i = 0; i < torchLights.Length; i++)
            {
                torchLights[i].enabled = false;
                dialogue.dialogueLines = FirstSentences;
                dialogue.ItemInteraction();
                
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        dialogue.OutOfRange();
        
    }

}
