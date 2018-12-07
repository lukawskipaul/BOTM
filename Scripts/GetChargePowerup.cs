using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChargePowerup : MonoBehaviour
{
    public PowerupManager thisInstance;
    private void OnTriggerEnter(Collider other)
    {
        thisInstance.UnlockPowerup(PowerupManager.Instance.pushBlock);
        Debug.Log("PushBlock Activated");
        Destroy(this);
    }
}
