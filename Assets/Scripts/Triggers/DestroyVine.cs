using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVine : MonoBehaviour
{
    [SerializeField]
    GameObject objectToDestroy;



    // Update is called once per frame
    void Update()
    {
        public void DestroyGameObject()
    {
        if (Input.GetButton("Attack") && gameObject.tag == "player")
        {
         Object.Destroy(objectToDestroy);
            
        }
    }


}
}
