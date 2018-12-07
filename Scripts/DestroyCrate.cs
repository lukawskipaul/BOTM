using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrate : MonoBehaviour {

    [SerializeField]
    GameObject crate, rock, creature;

    [SerializeField]
    Transform creatureMoveLocation;

    public string[] sentences;

    private DialogueSystem dialogueSystem;
    // Use this for initialization
    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rock)
        {
            Destroy(crate);
            creature.transform.position = creatureMoveLocation.position;
            creature.transform.rotation = creatureMoveLocation.rotation;
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.levitateMoveObject);
            PowerupManager.Instance.UnlockPowerup(PowerupManager.Instance.levitateObject);
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.ItemInteraction();

        }
    }
}
