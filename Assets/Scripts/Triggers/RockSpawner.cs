using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject RockPrefab;
    public float SpawnDelay = 5f;

    private GameObject activeRock;
    private bool shouldSpawnRock = false;
    private bool isOnCooldown = false;

    public void SpawnNewRock()
    {
        activeRock = GameObject.Instantiate(RockPrefab, this.transform);
        shouldSpawnRock = false;
        isOnCooldown = false;
        Debug.Log("Rock Spawned");
    }

    private void FixedUpdate()
    {
        CheckActiveRock();
    }

    private void CheckActiveRock()
    {
        if (activeRock == null && shouldSpawnRock == true)
        {
            SpawnNewRock();
        }
        else if (activeRock == null && shouldSpawnRock == false && isOnCooldown == false)
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    private IEnumerator SpawnCooldown()
    {
        isOnCooldown = true;
        float timePassed = 0f;
        while(timePassed < SpawnDelay)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }
        shouldSpawnRock = true;
    }
}
