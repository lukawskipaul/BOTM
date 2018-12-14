using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShot : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "LevitatableObject")
        {
            PowerupManager.Instance.levitateMoveObject.SetLevitatableObject(collision.gameObject);
            
        }
        Destroy(this.gameObject);
    }
}
