using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : ABSMatchUnit
{
    public override bool CanMatch => !Factory.IsMaxEvol(poolType);

    public override void OnMatched(int matchValue)
    {
        MatchCell curCell = Cell;
        OnDespawn();
        if (matchValue > 1)
        {
            Factory.CreateUnit(poolType + 1, TF.localPosition, TF.localRotation, TF.parent).Cell = curCell;
        }
    }
}
