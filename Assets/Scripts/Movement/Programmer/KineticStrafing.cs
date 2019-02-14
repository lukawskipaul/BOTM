using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticStrafing : Movement {

    public void UpdateMovement(GameObject gameObject)
    {
        gameObject.transform.Translate(this.Direction * this.Speed * Time.deltaTime);
    }
}
