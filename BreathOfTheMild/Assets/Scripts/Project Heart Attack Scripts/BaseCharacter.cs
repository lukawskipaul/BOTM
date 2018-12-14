using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter
{
    public string Name = "";
    // Character's name

    public int TurnPriority = -1;                              
    // Placeholder variable to determine order of Character actions

    public bool ActionSet = false;                  
    // Checks if Character has decided on an action

    public bool PerformedAction = false;
    // Variable to check if Character has already performed an action that turn

    public GameObject Selector;

    public List<BaseAttack> Attacks = new List<BaseAttack>();

    public float BaseHP;
    public float CurrentHP;

    public float BaseMP;
    public float CurrentMP;

    public float BaseATK;
    public float CurrentATK;

    public float BaseDEF;
    public float CurrentDEF;

    public int Stamina;
    public int Intellect;
    public int Dexterity;
    public int Agility;
    
    public BaseCharacter()
    {
        BaseHP = 500f;
        CurrentHP = BaseHP;

        BaseMP = 200f;
        CurrentMP = BaseMP;

        BaseATK = 100f;
        CurrentATK = BaseATK;

        BaseDEF = 50f;
        CurrentDEF = BaseDEF;

        Stamina = 10;
        Intellect = 10;
        Dexterity = 10;
        Agility = 10;
    }
}
