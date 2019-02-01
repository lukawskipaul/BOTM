using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TShield : MonoBehaviour
{
    [SerializeField]
    private Transform Spawnpoint;
    [SerializeField]
    private GameObject Shield;


    void  Update()
    {
        if (Input.GetKey("d"))
        {
            Instantiate(Shield, Spawnpoint.position,Spawnpoint.rotation);
        }
      
    }
}
