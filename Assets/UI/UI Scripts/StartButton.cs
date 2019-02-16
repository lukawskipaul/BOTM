﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad;

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
