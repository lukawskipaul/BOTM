using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : MonoBehaviour
{
    public string AttackName = "";
    public string AttackDescription = "";
    public float AttackBaseDamage = 0.0f;
    public float AttackCost = 0.0f;
    public ActionType AttackType = ActionType.NULL;
}