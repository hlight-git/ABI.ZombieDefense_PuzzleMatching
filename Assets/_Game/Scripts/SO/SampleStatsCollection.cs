using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleStatsCollection", menuName = "Scriptable Objects/Match Unit/Sample Stats Collection")]
public class SampleStatsCollection : SingletonScriptableObject<SampleStatsCollection>
{
    [SerializeField] List<SampleStatsItem> list;
    public T Get<T>(MatchType key) where T : SampleStats => (T) list.Find(x => x.key == key).value;
}

[System.Serializable]
public class SampleStatsItem
{
    public MatchType key;
    public SampleStats value;
}