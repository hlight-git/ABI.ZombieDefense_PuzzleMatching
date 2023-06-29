using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ZombieStats : CombatUnitStats<ZombieStats>
{
    public ZombieStats(float maxHealthPoint, float damage, float attackSpeed, float attackRange) : base(maxHealthPoint, damage, attackSpeed, attackRange)
    {
    }
}
