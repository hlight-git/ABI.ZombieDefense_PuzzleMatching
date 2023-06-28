using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CUnit Stats", menuName = "Scriptable Objects/Match Unit/CUnit Stats")]
public class CollectUnitSampleStats : SampleStats
{
    [SerializeField] CollectUnitStats stats;
    public CollectUnitStats Stats => stats;
}