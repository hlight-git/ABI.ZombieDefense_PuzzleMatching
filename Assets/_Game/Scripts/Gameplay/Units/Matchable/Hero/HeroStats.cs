using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HeroStats : CombatUnitStats<HeroStats>
{
    public HeroStats() : base()
    {

    }
    public HeroStats(HeroStats levelStats, HeroStats evolCoeff)
        : base(
            levelStats.MaxHealthPoint * evolCoeff.MaxHealthPoint,
            levelStats.Damage * evolCoeff.Damage,
            levelStats.AttackSpeed * evolCoeff.AttackSpeed,
            levelStats.AttackRange * evolCoeff.AttackRange
        )
    { }
}