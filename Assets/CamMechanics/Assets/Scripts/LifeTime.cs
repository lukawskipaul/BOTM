using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            Destroy(this.gameObject);
        }
    }
}
