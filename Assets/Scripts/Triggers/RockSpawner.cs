using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject RockPrefab;
    private GameObject activeRock;
    public float SpawnDelay;


    private void Start()
    {
        InvokeRepeating("SpawnNewRock", SpawnDelay);
    }

    public void SpawnNewRock()
    {
        activeRock = GameObject.Instantiate(RockPrefab, this.transform);
    }

    private void FixedUpdate()
    {
        CheckActiveRock();
    }

    private void CheckActiveRock()
    {
        if(activeRock == null)
        {
            SpawnNewRock();
        }
    }
}
