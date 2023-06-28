using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFactory : MatchUnitFactory<HeroData>
{
    readonly Dictionary<PoolType, HeroStats> statsCache;
    public PoolType MaxEvolType { get; private set; }
    public bool CanEvol(PoolType type) => type != MaxEvolType;

    public HeroFactory(MatchUnitData unitData) : base(unitData)
    {
        statsCache = new Dictionary<PoolType, HeroStats>();
        HeroSampleStats sampleStats = SampleStatsCollection.Ins.Get<HeroSampleStats>(unitData.UnitType);
        MaxEvolType = (PoolType)(unitData.UnitType + sampleStats.EvolCoeffsList.Count - 1);
        CreateStatsForEachEvol(sampleStats);
    }

    void CreateStatsForEachEvol(HeroSampleStats sampleStats)
    {
        for (int i = 0; i < sampleStats.EvolCoeffsList.Count; i++)
        {
            HeroStats stats = new HeroStats(sampleStats.LevelStatsList[unitData.Level - 1], sampleStats.EvolCoeffsList[i]);
            statsCache.Add((PoolType)(unitData.UnitType + i), stats);
        }
    }
    public HeroStats GetStats(PoolType type) => statsCache[type];
}