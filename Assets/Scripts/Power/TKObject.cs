using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TKObject : MonoBehaviour
{
    Animator enemyAnim;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyAnim = collision.gameObject.GetComponent<Animator>();
            enemyAnim.SetTrigger("Stun");
            Destroy(this.gameObject);
        }
        //added brendan wascher 2-28
        else if(collision.gameObject.tag == "Breakable")
        {
            //Destroy(this.gameObject);
        }
        //end add
        else
        {
            if (this.gameObject.tag == "ThrownObj")
            {
                this.gameObject.tag = "LevitatableObject";
            }
        }
    }
}
