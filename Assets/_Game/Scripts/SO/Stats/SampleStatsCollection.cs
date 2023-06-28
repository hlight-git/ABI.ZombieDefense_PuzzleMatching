using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleStatsCollection", menuName = "Scriptable Objects/Match Unit/Sample Stats Collection")]
public class SampleStatsCollection : SingletonScriptableObject<SampleStatsCollection>
{
    [SerializeField] List<SampleStatsItem> list;
    public T Get<T>(MatchUnitType key) where T : SampleStats => (T) list.Find(x => x.key == key).value;
}

[System.Serializable]
public class SampleStatsItem
{
    public MatchUnitType key;
    public SampleStats value;
}