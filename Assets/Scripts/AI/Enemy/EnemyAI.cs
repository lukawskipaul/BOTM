using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI {
    
    public EnemyAI()
    {

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
