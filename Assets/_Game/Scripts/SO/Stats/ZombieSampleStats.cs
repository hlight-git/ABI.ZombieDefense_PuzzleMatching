using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie Stats", menuName = "Scriptable Objects/Zombie Stats")]
public class ZombieSampleStats : ScriptableObject
{
    [SerializeField] ZombieStats stats;
    public ZombieStats Stats => stats;
}
