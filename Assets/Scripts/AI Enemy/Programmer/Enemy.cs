using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : IEnemy {
    public Vector3 Rotation { get; set; }
    protected Rigidbody enemyRigid;
    public Enemy(Rigidbody _enemy)
    {
        enemyRigid = _enemy;
    }
    public abstract void Start();
    
}
