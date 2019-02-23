using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadTurn : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        anim = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("isStrafing"))
        {
            Vector3 distToPlayer = player.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(distToPlayer, Vector3.up);
        }
    }
}
