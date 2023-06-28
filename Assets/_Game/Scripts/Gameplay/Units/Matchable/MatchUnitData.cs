using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatchUnitData
{
    public MatchType Type;
    public MatchUnitType UnitType;

    public MatchUnitData(MatchType type, MatchUnitType unitType)
    {
        Type = type;
        UnitType = unitType;
    }
}

public class CollectUnitData : MatchUnitData
{
    public CollectUnitData(MatchUnitType unitType) : base(MatchType.CollectUnit, unitType)
    {
    }
}

public class HeroData : MatchUnitData
{
    public int Level;

    public HeroData(MatchUnitType unitType) : base(MatchType.Hero, unitType)
    {
        Level = 1;
    }
}