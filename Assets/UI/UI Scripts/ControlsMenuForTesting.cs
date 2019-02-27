using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenuForTesting : MonoBehaviour
{
    public GameObject controlsCanvas;
    public string input;

    void Start()
    {
        controlsCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton(input))
        {
            controlsCanvas.gameObject.SetActive(true);
        }
        else
        {
            controlsCanvas.gameObject.SetActive(false);
        }
    }
}
