using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLayerCull : MonoBehaviour
{
    [SerializeField]
    private int layerNumber;
    private Light thisLight;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = this.gameObject.GetComponent<Light>();
        thisLight.cullingMask = layerNumber;
    }
}
