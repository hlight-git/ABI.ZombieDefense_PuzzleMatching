using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New SampleStats", menuName = "Scriptable Objects/Match Unit/Sample Stats")]
public class SampleStats : ScriptableObject
{
    [SerializeField] List<UnitStats> levelStatsList;
    [SerializeField] List<UnitStats> evolCoeffsList;

    public List<UnitStats> LevelStatsList => levelStatsList;
    public List<UnitStats> EvolCoeffsList => evolCoeffsList;
}