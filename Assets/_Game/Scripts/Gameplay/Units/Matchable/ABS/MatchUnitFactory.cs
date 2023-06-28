using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchUnitFactory
{
    readonly PoolType defaultSpawnType;

    public MatchUnitFactory(MatchUnitData unitData)
    {
        defaultSpawnType = (PoolType)unitData.UnitType;
    }

    public ABSMatchUnit CreateUnit(Vector3 position, Quaternion rotation, Transform parent)
        => CreateUnit(defaultSpawnType, position, rotation, parent);
    public ABSMatchUnit CreateUnit(PoolType type, Vector3 position, Quaternion rotation, Transform parent)
    {
        ABSMatchUnit unit = SimplePool.Spawn<ABSMatchUnit>(type, position, rotation, parent);
        unit.OnInit(this);
        return unit;
    }
}

public class MatchUnitFactory<UnitDataT> : MatchUnitFactory where UnitDataT : MatchUnitData
{
    protected UnitDataT unitData;

    public MatchUnitFactory(MatchUnitData unitData) : base(unitData)
    {
        this.unitData = (UnitDataT)unitData;
    }
}