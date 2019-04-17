using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainTK : MonoBehaviour
{
    [SerializeField]
    private Animator TKObjectFloat;
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private Telekinesis thisTK;
    [SerializeField]
    private DetectObject thisDO;
    //[SerializeField]
    //private SlowMoGameTime SlowMo;
    [SerializeField]
    private Transform PlayerLocation;
    [SerializeField]
    private GameObject ElectricExplosioin;

    private void OnEnable()
    {
        GivePlayerTK();
        Destroy(this.gameObject);
    }

    private void GivePlayerTK()
    {
        PlayerAnimator.SetTrigger("TakeDamage");
        TKObjectFloat.SetTrigger("shouldFloat");
        Instantiate(ElectricExplosioin, PlayerLocation.position, PlayerLocation.rotation);
        thisTK.enabled = true;
        thisDO.enabled = true;
        //SlowMo.SlowMo();
        Destroy(TKObjectFloat);
    }
}
