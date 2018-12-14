using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This was a simple little script to move an object's material on the renderer
/// to make it appear as if the object was moving.  This is how our lava moves.
/// This could be done with other aspects of smart materials, such as creating
/// waves on a water texture.
/// </summary>
public class MovingTextures : MonoBehaviour {

    [SerializeField]
    private float scrollX = 0.5f,
        scrollY = 0.5f;

    Renderer rndr;

	void Start ()
    {
        rndr = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;

        rndr.material.mainTextureOffset = new Vector2(offsetX, offsetY);
	}
}
