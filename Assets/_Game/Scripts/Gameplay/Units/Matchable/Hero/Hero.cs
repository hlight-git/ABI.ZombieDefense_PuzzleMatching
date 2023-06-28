using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ABSMatchUnit<HeroStats, HeroFactory>
{
    public override void OnInit(MatchUnitFactory factory)
    {
        base.OnInit(factory);
        CanMatch = this.factory.CanEvol(poolType);
        generalStats = this.factory.GetStats(poolType);
    }

    public override void OnMatched(int matchValue)
    {
        MatchCell curCell = Cell;
        OnDespawn();
        if (matchValue > 1)
        {
            factory.CreateUnit(poolType + 1, TF.localPosition, TF.localRotation, TF.parent).Cell = curCell;
        }
    }
}
