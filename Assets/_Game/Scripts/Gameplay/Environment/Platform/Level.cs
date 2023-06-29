using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level : GameUnit
{
    [SerializeField] MatchBoard matchBoard;
    [SerializeField, HideInInspector] List<PlatformRow> rows;
    [SerializeField, HideInInspector] int width;
    [SerializeField, HideInInspector] int height;

    public MatchBoard MatchBoard => matchBoard;
    public LinkedList<PlatformRow> RowLinkedList { get; private set; }

    private void Awake()
    {
        RowLinkedList = new LinkedList<PlatformRow>();
        for (int i = 0; i < rows.Count; i++)
        {
            RowLinkedList.AddLast(rows[i]);
        }
    }
    public void OnSpawn(int width, int height, List<PlatformRow> rows, int matchBoardWidth, int matchBoardHeight)
    {
        this.width = width;
        this.height = height;
        this.rows = rows;
        matchBoard.OnSpawn(matchBoardWidth, matchBoardHeight);
    }
    private void OnARowFloatingToEndPoint(PlatformRow row)
    {
        if (TF.position.z > row.TF.position.z)
        {
            row.TF.position += height * TF.forward;
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
