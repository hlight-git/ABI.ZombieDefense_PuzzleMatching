using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SampleStats : ScriptableObject { }
public class SampleStats<UnitStatsT> : SampleStats where UnitStatsT : UnitStats
{
    [SerializeField] List<UnitStatsT> levelStatsList;
    [SerializeField] List<UnitStatsT> evolCoeffsList;

    public List<UnitStatsT> LevelStatsList => levelStatsList;
    public List<UnitStatsT> EvolCoeffsList => evolCoeffsList;
}