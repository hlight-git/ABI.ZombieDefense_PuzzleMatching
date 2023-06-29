using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ZombieStats : CombatUnitStats<ZombieStats>
{
    public float MoveSpeed;
    public ZombieStats(float maxHealthPoint, float damage, float attackSpeed, float attackRange, float moveSpeed) : base(maxHealthPoint, damage, attackSpeed, attackRange)
    {
        MoveSpeed = moveSpeed;
    }
}