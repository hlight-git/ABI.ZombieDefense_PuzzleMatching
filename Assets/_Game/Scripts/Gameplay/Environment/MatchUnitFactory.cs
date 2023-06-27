using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchType
{
    Melee1 = PoolType.MU_Melee1_0,
    Melee2 = PoolType.MU_Melee2_0,
    Range1 = PoolType.MU_Range1_0,
    Range2 = PoolType.MU_Range2_0,
    Bonus = PoolType.MU_Bonus,
}

public class MatchUnitFactory
{
    readonly MatchUnitData unitData;
    readonly PoolType defaultSpawnType;
    readonly Dictionary<PoolType, UnitStats> statsCache;

    public PoolType MaxEvolType { get; private set; }

    public bool IsMaxEvol(PoolType type) => type == MaxEvolType;

    public MatchUnitFactory(MatchUnitData unitData)
    {
        this.unitData = unitData;
        defaultSpawnType = (PoolType)unitData.MatchType;
        statsCache = new Dictionary<PoolType, UnitStats>();

        SampleStats sampleStats = SampleStatsCollection.Ins.Get<SampleStats>(unitData.MatchType);
        MaxEvolType = (PoolType)(unitData.MatchType + sampleStats.EvolCoeffsList.Count - 1);
        CreateStatsForEachEvol(sampleStats);
    }
    void CreateStatsForEachEvol(SampleStats sampleStats)
    {
        for (int i = 0; i < sampleStats.EvolCoeffsList.Count; i++)
        {
            UnitStats unitStats = new UnitStats();
            unitStats.Combine(sampleStats.LevelStatsList[unitData.Level - 1], sampleStats.EvolCoeffsList[i]);
            statsCache.Add((PoolType)(unitData.MatchType + i), unitStats);
        }
    }
    public void Boost()
    {

    }

    public T GetStats<T>(PoolType type) where T : UnitStats => (T) statsCache[type];
    public ABSMatchUnit CreateUnit(Vector3 position, Quaternion rotation, Transform parent)
        => CreateUnit(defaultSpawnType, position, rotation, parent);
    public ABSMatchUnit CreateUnit(PoolType type, Vector3 position, Quaternion rotation, Transform parent)
    {
        ABSMatchUnit unit = SimplePool.Spawn<ABSMatchUnit>(type, position, rotation, parent);
        unit.OnInit(this);
        return unit;
    }
}