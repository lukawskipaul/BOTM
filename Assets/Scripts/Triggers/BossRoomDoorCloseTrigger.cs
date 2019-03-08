using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoorCloseTrigger : MonoBehaviour
{
    public Animator BossDoor;
    public GameObject TriggerZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BossDoor.SetBool("Boss_Door_Set_Open", false);
            Destroy(TriggerZone);
            AkSoundEngine.PostEvent("Play_SlidingDoorClose", gameObject);
        }
    }
}
