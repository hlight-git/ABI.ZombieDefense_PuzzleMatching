using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats
{
    public float MaxHealthPoint;

    public UnitStats()
    {

    }
    public UnitStats(float maxHealthPoint)
    {
        MaxHealthPoint = maxHealthPoint;
    }
    public T Clone<T>() where T : UnitStats => (T) MemberwiseClone();
}
public class UnitStats<SelfT> : UnitStats where SelfT : UnitStats<SelfT>
{
    public UnitStats() : base()
    {

    }
    public UnitStats(float maxHealthPoint) : base(maxHealthPoint)
    {
    }

    public virtual void Combine(SelfT statsA, SelfT statsB)
    {
        MaxHealthPoint = statsA.MaxHealthPoint + statsB.MaxHealthPoint;
    }
}