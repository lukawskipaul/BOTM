using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSphereOfDeath : MonoBehaviour
{
    private PlayerRespawnScript playerRespawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Murdered");
            playerRespawn = other.GetComponent<PlayerRespawnScript>();
            playerRespawn.RespawnPlayer();            
        }
    }
}
