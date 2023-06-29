using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level : GameUnit
{
    [SerializeField] MatchBoard matchBoard;
    //[SerializeField, HideInInspector] List<PlatformRow> rows;
    //[SerializeField, HideInInspector] int width;
    //[SerializeField, HideInInspector] int height;

    //[SerializeField, HideInInspector] int zombieSmallAmount;
    //[SerializeField, HideInInspector] int zombieMediumAmount;
    //[SerializeField, HideInInspector] int zombieLargeAmount;
    [SerializeField, HideInInspector] List<PlatformRow> rows;
    [SerializeField, HideInInspector] LevelData levelData;
    public MatchBoard MatchBoard => matchBoard;
    public LinkedList<PlatformRow> RowLinkedList { get; private set; }
    public List<PlatformRow> Rows { get => rows; set => rows = value; }
    public LevelData LevelData { get => levelData; set => levelData = value; }

    private void Awake()
    {
        RowLinkedList = new LinkedList<PlatformRow>();
        for (int i = 0; i < Rows.Count; i++)
        {
            RowLinkedList.AddLast(Rows[i]);
        }
    }
    private void OnARowFloatingToEndPoint(PlatformRow row)
    {
        if (TF.position.z > row.TF.position.z)
        {
            row.TF.position += levelData.Height * TF.forward;
            RowLinkedList.RemoveFirst();
            RowLinkedList.AddLast(row);
        }
        if (GameManager.IsState(GameState.MainMenu))
        {
            row.Floating(OnARowFloatingToEndPoint);
        }
    }
    public void StartFloating()
    {
        LinkedListNode<PlatformRow> node = RowLinkedList.First;
        while (node != null)
        {
            node.Value.Floating(OnARowFloatingToEndPoint);
            node = node.Next;
        }
    }
    /// <summary>
    /// A way to make rows stop floating.
    /// </summary>
    public async void StopFloating()
    {
        LinkedListNode<PlatformRow> node = RowLinkedList.First;
        while (node != null)
        {
            await node.Value.FloatingTask;
            node = node.Next;
        }
    }
    /// <summary>
    /// A way to make rows stop floating.
    /// </summary>
    /// <returns>A list of tween tasks of rows</returns>
    public List<Task> GetFloatingTasks()
    {
        List<Task> tasks = new List<Task>();
        LinkedListNode<PlatformRow> node = RowLinkedList.First;
        while (node != null)
        {
            tasks.Add(node.Value.FloatingTask);
            node = node.Next;
        }
        return tasks;
    }
}
