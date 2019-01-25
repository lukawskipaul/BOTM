using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private bool isActivated;

    public void SetIsActivated(bool value)
    {
        isActivated = value;
    }
}
