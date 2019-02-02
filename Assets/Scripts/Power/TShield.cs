using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TShield : MonoBehaviour
{
    [SerializeField]
    private Transform spawnpoint;
    [SerializeField]
    private GameObject shield;


    void  Update()
    {
        if (Input.GetButtonDown("Shield"))
        {
            GameObject shieldClone;
            shieldClone = Instantiate(shield, spawnpoint.position, spawnpoint.rotation) as GameObject;
            shieldClone.transform.SetParent(this.transform, true);
        }
      
    }
}
