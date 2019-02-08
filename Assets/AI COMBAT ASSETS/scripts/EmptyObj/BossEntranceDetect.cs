using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class BossEntranceDetect : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Collider>().isTrigger = true;//Sets collider to a trigger
    }
    /// <summary>
    /// When the player enters the trigger, the gameobject this script is attached to is disabled & the boss detects the player.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            Debug.Log(boss);
            boss.GetComponent<Animator>().SetTrigger("PlayerDetected");
            this.gameObject.SetActive(false);
        }
    }
}
