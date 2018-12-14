using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPowerZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PowerupManager.Instance.UnlockAll();

        }
    }
}
