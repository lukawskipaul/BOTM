using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2f;


    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        if (lifeTime <= 0)
        {
            Destruction();
        }
    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
