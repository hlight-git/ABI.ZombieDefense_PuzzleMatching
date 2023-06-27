using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUnit : ABSMatchUnit<UnitStats>
{
    public override bool CanMatch => true;

    public override void OnMatched(int matchValue)
    {
        OnDespawn();
    }
}
