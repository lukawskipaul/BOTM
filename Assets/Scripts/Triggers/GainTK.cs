﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainTK : MonoBehaviour
{
    [SerializeField]
    private Telekinesis thisTK;
    [SerializeField]
    private DetectObject thisDO;
    [SerializeField]
    private TKShieldController thisTKSC;

    private void OnEnable()
    {
        GivePlayerTK();
        Destroy(this.gameObject);
    }

    private void GivePlayerTK()
    {
        thisTK.enabled = true;
        thisDO.enabled = true;
        thisTKSC.enabled = true;
    }
}
