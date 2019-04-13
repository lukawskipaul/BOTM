using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRockDestroy : MonoBehaviour
{
    [SerializeField]
    private GameObject DestroyFeedback;
    private void OnDestroy()
    {
        Instantiate(DestroyFeedback,this.transform.position,this.transform.rotation);
    }
}
