using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningExplosion : MonoBehaviour
{
    [SerializeField]
    private float lifeDuration = 4f;
    [SerializeField]
    private float lightDuration = 1f;
    [SerializeField]
    private float intensityRate = 2.5f;
    private Light light;
    private void OnEnable()
    {
        light = this.gameObject.GetComponent<Light>();
        StartCoroutine("StartKill");
        StartCoroutine("LightShock");
    }

    private IEnumerator StartKill()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(this.gameObject);
    }

    private IEnumerator LightShock()
    {
        while(lightDuration > 0)
        {
            light.intensity = light.intensity * intensityRate*Time.deltaTime+light.intensity;
            lightDuration -= Time.deltaTime;
            yield return null;
        }
        while(light.intensity > 0)
        {
            light.intensity -= Time.deltaTime * 2.8f * intensityRate;
            yield return null;
        }
    }
}
