using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public Image crystal;

    bool fading = true;

    public CanvasRenderer rend;

    private float alpha;
	
	// Update is called once per frame
	void FixedUpdate()
    {
        alpha = rend.GetAlpha();
		if(fading == true && alpha == 1)
        {
            Debug.Log("hit fade");
            crystal.CrossFadeAlpha(.4f, 5f, false);
            fading = false;
        }

        if(fading == false && alpha == .4f)
        {
            Debug.Log("hit not fade");
            crystal.CrossFadeAlpha(1, 5f, false);
            fading = true;
        }
	}
}
