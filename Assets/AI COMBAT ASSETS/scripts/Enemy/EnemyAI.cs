using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI {
    protected int Health;
    
    public EnemyAI(int _health)
    {
        Health = _health;
    }
    /// <summary>
    /// Calculate how much Health the enemy has after taking damage 
    /// </summary>
    /// <param name="_damage">How much health to take away</param>
    public void TakeDamage(int _damage)
    {
        Health -= _damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }
    /// <summary>
    /// Retrieves the amount of heath the enemy has
    /// </summary>
    /// <returns>The enemy's Health</returns>
    public int GetHealth()
    {
        return Health;
    }
    /// <summary>
    /// Calculate the square distance between the Player and the Enemy
    /// </summary>
    /// <param name="_enemy">The Enemy Gameobject</param>
    /// <param name="_player">The Player Gameobject</param>
    /// <returns>returns the squared distance between player & enemy</returns>
    public float SquaredDistanceToPlayer(GameObject _enemy,GameObject _player)
    {
        Vector3 distVec = _player.transform.position - _enemy.transform.position;
        return distVec.sqrMagnitude;
    }

}
