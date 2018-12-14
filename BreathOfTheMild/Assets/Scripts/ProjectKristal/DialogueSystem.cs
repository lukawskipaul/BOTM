using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public Text dialogueText;

    public GameObject dialogBox;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public string[] dialogueLines;

    public bool letterIsMulitplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;

    public string InteractButton;



	// Use this for initialization
	void Start ()
    {
        dialogueText.text = "";
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ItemInteraction()
    {
        outOfRange = false;
        dialogBox.SetActive(true);
        //Can include a required button to be pressed to interact
        if(!dialogueActive)
        {
            dialogueActive = true;
            StartCoroutine(StartDialogue());
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if(outOfRange == false)
        {
            int dialogueLength = dialogueLines.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMulitplied)
            {
                if(!letterIsMulitplied)
                {
                    letterIsMulitplied = true;
                    StartCoroutine(DisplayString(dialogueLines[currentDialogueIndex++]));

                    if(currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }

                }
                yield return 0;

            }

            while(true)
            {
                //Can put input required here
                if(dialogueEnded == false && Input.GetButtonDown(InteractButton))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }

        
    }
    private IEnumerator DisplayString(string stringToDisplay)
    {
        if(outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while(currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;
                if(currentCharacterIndex < stringLength)
                {
                    if(Input.GetButtonDown(InteractButton))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while(true)
            {
                if(Input.GetButtonDown(InteractButton))
                {
                    break;
                }
                yield return 0;

            }
            dialogueEnded = false;
            letterIsMulitplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogBox.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if(outOfRange == true)
        {
            letterIsMulitplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogBox.SetActive(false);
        }
    }
}
