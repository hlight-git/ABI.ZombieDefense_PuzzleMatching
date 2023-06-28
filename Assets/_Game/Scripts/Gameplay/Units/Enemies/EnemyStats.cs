using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStats : CombatUnitStats<EnemyStats>
{
    public EnemyStats(float maxHealthPoint, float damage, float attackSpeed, float attackRange) : base(maxHealthPoint, damage, attackSpeed, attackRange)
    {
    }
}
