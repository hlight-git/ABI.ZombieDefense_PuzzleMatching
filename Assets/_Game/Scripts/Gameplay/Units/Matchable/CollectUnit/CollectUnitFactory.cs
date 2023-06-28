using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUnitFactory : MatchUnitFactory<MatchUnitData>
{
    public CollectUnitStats Stats { get; private set; }
    public CollectUnitFactory(MatchUnitData unitData) : base(unitData)
    {
        Stats = new CollectUnitStats(SampleStatsCollection.Ins.Get<CollectUnitSampleStats>(unitData.UnitType).Stats.MaxHealthPoint);
    }
}