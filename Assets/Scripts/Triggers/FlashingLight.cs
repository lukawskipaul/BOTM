using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    [SerializeField]
    private Light ThisLight;
    [SerializeField]
    private float rateChange = 1f;
    private float baseIntensity;

    private bool shouldSwitch = true;
    private bool isFirstTake = true;

    private void Start()
    {
        baseIntensity = ThisLight.intensity;
    }
    private void Update()
    {
        ChangeLight();
    }

    private void ChangeLight()
    {
        if (shouldSwitch) //decreasing in intesity
        {
            ThisLight.intensity -= (Time.deltaTime * rateChange);
            CheckFirstTake();
            if (ThisLight.intensity <= 0)
                shouldSwitch = false;
        }
        else
        {
            ThisLight.intensity += (Time.deltaTime * rateChange);
            CheckFirstTake();
            if (ThisLight.intensity >= baseIntensity)
                shouldSwitch = true;
            
        }
        //Debug.Log(ThisLight.intensity);
    }
    private void CheckFirstTake()
    {
        if (isFirstTake)
        {
            baseIntensity = ThisLight.intensity;
            isFirstTake = false;
        }
    }
}
