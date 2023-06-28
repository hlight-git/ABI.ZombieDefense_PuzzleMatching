using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CollectUnitStats : UnitStats<CollectUnitStats>
{
    public CollectUnitStats() : base()
    {

    }
    public CollectUnitStats(float maxHealthPoint) : base(maxHealthPoint)
    {
    }
}