using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Small,
    Medium,
    Large
}

[System.Serializable]
public class UnitStats
{
    public float MaxHealthPoint;
    public float Damage;
    public float AttackSpeed;
    public float AttackRange;
    public virtual void Combine<SelfT>(SelfT levelStat, SelfT evolCoeff) where SelfT : UnitStats
    {
        MaxHealthPoint = levelStat.MaxHealthPoint * evolCoeff.MaxHealthPoint;
        Damage = levelStat.Damage * evolCoeff.Damage;
        AttackSpeed = levelStat.AttackSpeed * evolCoeff.AttackSpeed;
        AttackRange = levelStat.AttackRange * evolCoeff.AttackRange;
    }
}

public class HeroStats : UnitStats
{
}
//public class ItemSampleStat : UnitStats
//{

//}

//public class BoosterSampleStat : UnitStats
//{

//}
//public class EnemySampleStat : UnitStats
//{

//}
