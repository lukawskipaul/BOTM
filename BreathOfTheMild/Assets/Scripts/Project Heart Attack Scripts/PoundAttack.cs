using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundAttack : BaseAttack
{
    public PoundAttack()
    {
        AttackName = "Pound";
        AttackDescription = "A full-forced heavy attack";
        AttackBaseDamage = 20.0f;
        AttackCost = 10.0f;
    }
}
