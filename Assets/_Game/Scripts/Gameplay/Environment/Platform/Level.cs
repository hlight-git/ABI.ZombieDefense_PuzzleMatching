using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level : GameUnit
{
    int width;
    int height;

    public int MatchBoardWidth { get; private set; }
    public int MatchBoardHeight { get; private set; }
    public LinkedList<PlatformRow> RowLinkedList { get; private set; }

    public void OnInit(int width, int height, LinkedList<PlatformRow> rowLinkedList, int matchBoardWidth, int matchBoardHeight)
    {
        this.width = width;
        this.height = height;
        RowLinkedList = rowLinkedList;
        MatchBoardWidth = matchBoardWidth;
        MatchBoardHeight = matchBoardHeight;
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
