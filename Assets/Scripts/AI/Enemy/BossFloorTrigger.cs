using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class BossFloorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
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
        if (other.tag == "Player")
        {
            //Awake the boss when entering box
            Debug.Log("isAwake = true");
            bossAnim.SetBool("isAwake",true);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Disable gameobject when leaving this triggerbox
            this.gameObject.SetActive(false);
        }
        
    }
}
