using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerRoomTrigger : MonoBehaviour
{
    [SerializeField]
    private Rigidbody PipeBlock;

    [SerializeField]
    private GameObject LockerRoomTriggerBreak;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PipeBlock.isKinematic = false;
            //player stun animation here
            LockerRoomTriggerBreak.SetActive(true);
            Destroy(this.gameObject.GetComponent<Collider>());
        }
    }
}
