using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy : BaseCharacter
{
    public enum Type
    {
        GRASS,
        FIRE,
        WATER,
        ELECTRIC
    }

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERRARE
    }

    public Type EnemyType = Type.GRASS;
    public Rarity EnemyRarity = Rarity.COMMON;
}
