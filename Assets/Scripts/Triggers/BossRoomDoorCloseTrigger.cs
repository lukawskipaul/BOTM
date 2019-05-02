using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoorCloseTrigger : MonoBehaviour
{
    public Animator BossDoor1;
    public Animator BossDoor2;
    public Animator BossDoor3;
    public GameObject TriggerZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BossDoor1.SetBool("Boss_Door_Set_Open", false);
            BossDoor2.SetBool("Boss_Door_Set_Open", false);
            BossDoor3.SetBool("Boss_Door_Set_Open", false);
            TriggerZone.SetActive(false);
            AkSoundEngine.PostEvent("Play_SlidingDoorClose", gameObject);
        }
    }
}
