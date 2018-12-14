using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActionType
{
    MELEE_ATTACK,
    RANGED_ATTACK,
    MAGIC,
    GUARD,
    NULL
};

[System.Serializable]
public class HandleTurn
{
    public ActionType Action;
    // The type of action being performed

    public string Type;
    // Hero, Enemy, Boss, etc.

    public string AttackersName; 
    // name of attacker

    public GameObject AttackersGameObject;  
    // who attacks

    public GameObject AttackersTarget;      
    // who gets attacked

    public int TurnPriority;
    // the turn priority of the attacker

    public BaseAttack ChosenAttack;
    // which attack is performed

    public HandleTurn()
    {
        Action = ActionType.NULL;
    }
}
