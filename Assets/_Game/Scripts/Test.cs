using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public enum EnumTest
{
    Val0,
    Val1, Val2, Val3, Val4, Val5,
}
public class Test : MonoBehaviour
{
    //public TestSO unitStats;
    //public TestSO unitStats2;
    private void Start()
    {
        EnumTest[] t = new EnumTest[10];
        print(t[0] + " " + t[1]);
        //unitStats2 = Instantiate(unitStats);
        //print(unitStats2.LevelStatsList.Count);
        //print(unitStats2.EvolCoeffsList.Count);

        //var watch = System.Diagnostics.Stopwatch.StartNew();
        //int tmp;
        //for (int i = 0; i < 1000000; i++)
        //{
        //    tmp = (int)(object)EnumTest.Val;
        //}

        //watch.Stop();
        //print($"Loop 1 Execution Time: {watch.ElapsedMilliseconds} ms");

        //watch.Reset();
        //watch.Start();

        //for (int i = 0; i < 1000000; i++)
        //{
        //    tmp = System.Convert.ToInt32(EnumTest.Val);
        //}

        //watch.Stop();
        //print($"Loop 2 Execution Time: {watch.ElapsedMilliseconds} ms");
    }
}

public class Hehe
{
    public int Tmp;
    public List<EnumTest> data = new List<EnumTest>();
    public void Add()
    {
    }
}