using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoorTrigger : MonoBehaviour
{
    public GameObject ThisBattery;
    public Animator BossDoor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LevitatableObject" || other.tag == "ThrownObj")
        {
            if(other.gameObject.name == "Battery&&")
            {
                Destroy(other.gameObject);
                TurnOnBattery();
            }
        }
    }
    private void TurnOnBattery()
    {
        ThisBattery.gameObject.SetActive(true);
        BossDoor.SetBool("Boss_Door_Set_Open", true);
        //AkSoundEngine.PostEvent("Play_MetalDoorOpen", gameObject);
    }
}
