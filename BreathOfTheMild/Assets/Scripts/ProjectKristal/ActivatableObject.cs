using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ActivatableObject : MonoBehaviour, IActivatable
{
    [SerializeField]
    string nameText;

    
    //Animator anim;

    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public string NameText
    {
        get
        {
            return nameText;
        }
    }

    public void DoActivate()
    {
        // whatever we want to happen


    }
}
