using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipTrigger : MonoBehaviour
{
    public GameObject toolTipTrigger;
    public string toolTipAnimationName;
    public Animator toolTipAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //toolTipAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            toolTipAnimation.Play(toolTipAnimationName);
            toolTipTrigger.gameObject.SetActive(false);
        }
    }
}
