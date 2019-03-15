using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth bossHealth;
    [SerializeField]
    private GameObject endLight;
    [SerializeField]
    private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
    }

    private void CheckEndGame()
    {
        if(bossHealth.isDead)
        {
            endLight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && bossHealth.isDead)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
