using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainTK : MonoBehaviour
{
    [SerializeField]
    private RootMotionMovementController movement;
    [SerializeField]
    private Animator TKObjectFloat;
    [SerializeField]
    private Animator PlayerAnimator;
    [SerializeField]
    private Telekinesis thisTK;
    [SerializeField]
    private DetectObject thisDO;
    [SerializeField]
    private SlowMoGameTime SlowMo;
    [SerializeField]
    private Transform PlayerLocation;
    [SerializeField]
    private GameObject ElectricExplosioin;
    [SerializeField]
    private float timeToStop;
    private void Update()
    {
        SaveTKAbilityInfo();
    }
    private void OnEnable()
    {
        GivePlayerTK();
        Destroy(this.gameObject);
    }
    /// <summary>
    /// When player dies and already has the Telekinesis ability, give the player TK abilities again
    /// </summary>
    private void SaveTKAbilityInfo()
    {
        if (PlayerPrefs.GetInt("TKAcquired") == 1)
        {
            if (!TKObjectFloat || !thisTK || !thisDO)
            {
                //Enable Telekinesis abilities
                TKObjectFloat.enabled = true;
                thisTK.enabled = true;
                thisDO.enabled = true;
            }
        }
    }
    private void GivePlayerTK()
    {
        TKObjectFloat.enabled = true;
        PlayerAnimator.SetTrigger("TakeDamage");
        TKObjectFloat.SetTrigger("shouldFloat");
        Instantiate(ElectricExplosioin, PlayerLocation.position, PlayerLocation.rotation);
        thisTK.enabled = true;
        thisDO.enabled = true;
        SlowMo.SlowMo();
        Destroy(TKObjectFloat);
        movement.DisableMovement(timeToStop);
        PlayerPrefs.SetInt("TKAcquired",1);//Save data that TK has been picked up already
    }
}
