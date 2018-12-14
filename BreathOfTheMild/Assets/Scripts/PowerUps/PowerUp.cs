using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    private bool isActivated;
    private bool isUnlocked;
    private string powerName = "Implement PowerName Property";

    public virtual string PowerName
    {
        get
        {
            return powerName;
        }
    }

    public bool IsActivated
    {
        get
        {
            return isActivated;
        }

        set
        {
            isActivated = value;
        }
    }

    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }

        set
        {
            isUnlocked = value;
        }
    }

    public virtual void UsePower()
    {

    }

    public virtual void UsePower(GameObject gameObject)
    {

    }
}
