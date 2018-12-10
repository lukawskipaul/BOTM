using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTimeScale : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
