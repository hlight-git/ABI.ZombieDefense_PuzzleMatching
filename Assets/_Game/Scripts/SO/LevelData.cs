using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] int width = 6;
    [SerializeField] int height = 40;

    [SerializeField] int matchBoardWidth = 6;
    [SerializeField] int matchBoardHeight = 5;

    [SerializeField] int zombieSmallAmount;
    [SerializeField] int zombieMediumAmount;
    [SerializeField] int zombieLargeAmount;

    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int ZombieSmallAmount { get => zombieSmallAmount; set => zombieSmallAmount = value; }
    public int ZombieMediumAmount { get => zombieMediumAmount; set => zombieMediumAmount = value; }
    public int ZombieLargeAmount { get => zombieLargeAmount; set => zombieLargeAmount = value; }
    public int MatchBoardWidth { get => matchBoardWidth; set => matchBoardWidth = value; }
    public int MatchBoardHeight { get => matchBoardHeight; set => matchBoardHeight = value; }
}
