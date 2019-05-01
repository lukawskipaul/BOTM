using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveJournalEntry : MonoBehaviour
{
    [SerializeField]
    private GameObject thisJournalEntry;
    private void OnEnable()
    {
        thisJournalEntry.SetActive(true);
    }
}
