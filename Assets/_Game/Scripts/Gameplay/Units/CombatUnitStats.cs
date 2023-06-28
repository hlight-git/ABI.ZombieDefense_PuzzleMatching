using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CombatUnitStats<SelfT> : UnitStats<SelfT> where SelfT : CombatUnitStats<SelfT>
{
    public float Damage;
    [Tooltip("Attack speed")]
    public float AttackSpeed;
    [Tooltip("Attack range")]
    public float AttackRange;
    public CombatUnitStats() : base()
    {

    }
    public CombatUnitStats(float maxHealthPoint, float damage, float attackSpeed, float attackRange) : base(maxHealthPoint)
    {
        Damage = damage;
        AttackSpeed = attackSpeed;
        AttackRange = attackRange;
    }

    public override void Combine(SelfT statsA, SelfT statsB)
    {
        base.Combine(statsA, statsB);
        Damage = statsA.Damage + statsB.Damage;
        AttackSpeed = statsA.AttackSpeed + statsB.AttackSpeed;
        AttackRange = statsB.AttackRange + statsB.AttackRange;
    }
}