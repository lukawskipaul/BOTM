using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField]
    private float topRange = 2.0f;
    [SerializeField]
    private float lowRange = 0.5f;
    private Light ThisLight;
    // Start is called before the first frame update
    void Start()
    {
        ThisLight = this.gameObject.GetComponent<Light>();
        StartCoroutine("LightFlicker");
    }

    private IEnumerator LightFlicker()
    {
        while (true)
        {
            ThisLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(lowRange, topRange));
            ThisLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(0f, lowRange));
        }
    }
}
