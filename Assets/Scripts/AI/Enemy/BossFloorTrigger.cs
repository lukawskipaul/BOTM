using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class BossFloorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private string playerTag = "Player";

    private Animator bossAnim;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        bossAnim = boss.GetComponent<Animator>();//boss animator ref
        collider = this.GetComponent<Collider>();//collider ref
        collider.isTrigger = true;//Set box to trigger
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            //Awake the boss when entering box
            bossAnim.SetBool("isAwake",true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            //Disable gameobject when leaving this triggerbox
            this.gameObject.SetActive(false);
        }
        
    }
}
