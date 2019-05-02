using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndState : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth bossHealth;
    [SerializeField]
    private GameObject endLight;
    [SerializeField]
    private Animator EndDoor;
    void Update()
    {
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if (bossHealth.isDead)
        {
            endLight.SetActive(true);
            EndDoor.SetBool("Boss_Door_Set_Open", true);
            Destroy(this.gameObject);
        }
    }
}
