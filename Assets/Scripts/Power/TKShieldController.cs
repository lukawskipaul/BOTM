using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKShieldController : MonoBehaviour
{
    [SerializeField]
    private Transform spawnpoint;
    [SerializeField]
    private GameObject shield;

    private GameObject shieldClone;

    void  Update()
    {
        if (Input.GetButtonDown("Shield"))
        {
            shieldClone = Instantiate(shield, spawnpoint.position, spawnpoint.rotation) as GameObject;
            shieldClone.transform.SetParent(this.transform, true);
        }
        else if (Input.GetButtonUp("Shield") && shieldClone != null)
        {
            Destroy(shieldClone);
        }
      
    }
}
