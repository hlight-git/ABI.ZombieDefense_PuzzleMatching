using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUnit : ABSMatchUnit<CollectUnitStats, CollectUnitFactory>
{
    public override void OnInit(MatchUnitFactory factory)
    {
        base.OnInit(factory);
        CanMatch = true;
        generalStats = this.factory.Stats;
    }

    public override void OnMatched(int matchValue)
    {
        OnDespawn();
    }
}
