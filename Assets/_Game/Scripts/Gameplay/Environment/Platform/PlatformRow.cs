using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformRow : GameUnit
{
    Tween floatTweenZ;
    public PlatformTile[] Tiles { get; set; }
    public Task FloatingTask => floatTweenZ.AsyncWaitForCompletion();
    public void Floating(Action<PlatformRow> OnReachedEndValueAction)
    {
        floatTweenZ = TF.DOMoveZ(TF.position.z - 1, 1)
            .SetSpeedBased(true)
            .SetEase(Ease.Linear)
            .OnComplete(() => OnReachedEndValueAction(this));
    }
}
