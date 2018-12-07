using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : BaseAttack
{
    public SlashAttack()
    {
        AttackName = "Slash";
        AttackDescription = "A fast slash attack with a bladed weapon";
        AttackBaseDamage = 10.0f;
        AttackCost = 5.0f;
    }

}
