using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor2 : MonoBehaviour
{
    public Animator bossDoor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bossDoor.SetBool("Boss_Door_Set_Open", true);
            Destroy(this.gameObject);
        }
    }
}
