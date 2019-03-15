using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorRestart : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Sparks;

    [SerializeField]
    private GameObject SparkSystem;

    [SerializeField]
    private GameObject ThisBattery;

    [SerializeField]
    private GameObject DoorTriggerZone;

    [SerializeField]
    private GameObject LobbyLights;

    [SerializeField]
    private GameObject PlayerLight;

    [SerializeField]
    private Material DrainedMaterial;

    [SerializeField]
    private float timeForRestart = 3f;

    private float passedTime = 0f;
    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");
        if(other.tag == "LevitatableObject" && other.name == "Battery&&" && hasBeenTriggered == false)
        {
            BatteryHitTrigger(other);
        }
    }

    private void BatteryHitTrigger(Collider other)
    {
        //Debug.Log("Trigger2");
        hasBeenTriggered = true;
        ThisBattery.SetActive(true);
        Destroy(other.gameObject);
        SparkSystem.SetActive(true);
        Sparks.Play();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while(passedTime < timeForRestart)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }
        Renderer BatteryRenderer = ThisBattery.GetComponent<Renderer>();
        BatteryRenderer.material = DrainedMaterial;
        Sparks.Stop();
        SparkSystem.SetActive(false);
        LobbyLights.SetActive(true);
        PlayerLight.SetActive(false);
        //Turn on lights here
    }
}
