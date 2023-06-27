using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformRow : GameUnit
{
    [SerializeField] PlatformTile tilePrefab;
    Tween floatTweenZ;
    public PlatformTile[] Tiles { get; private set; }
    public Task FloatingTask => floatTweenZ.AsyncWaitForCompletion();

    public void OnInit(int width)
    {
        Tiles = new PlatformTile[width];
        for (int r = 0; r < width; r++)
        {
            Tiles[r] = Instantiate(
                tilePrefab,
                TF.position + r * TF.right,
                Quaternion.identity,
                TF
            );
        }
    }
    public void Floating(Action<PlatformRow> OnReachedEndValueAction)
    {
        floatTweenZ = TF.DOMoveZ(TF.position.z - 1, 1)
            .SetSpeedBased(true)
            .SetEase(Ease.Linear)
            .OnComplete(() => OnReachedEndValueAction(this));
    }
}
