using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField]
    private Transform objectToLookAt;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectToLookAt);
    }
}
