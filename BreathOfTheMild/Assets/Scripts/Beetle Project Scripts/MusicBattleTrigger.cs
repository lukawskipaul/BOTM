using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBattleTrigger : MonoBehaviour {


    public AudioSource battleMusic;



	// Use this for initialization
	void Start ()
    {
        battleMusic = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        battleMusic.Play();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        
    }
}
