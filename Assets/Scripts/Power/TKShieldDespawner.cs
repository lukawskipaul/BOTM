using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKShieldDespawner : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonUp("Shield"))
        {
            Destroy(this.gameObject);
        }
    }
}
