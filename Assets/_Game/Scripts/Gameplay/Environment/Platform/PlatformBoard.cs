using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlatformBoard : GameUnit
{
    [SerializeField] PlatformRow rowPrefab;
    [SerializeField] int width;
    [SerializeField] int height;
    public LinkedList<PlatformRow> RowLinkedList { get; private set; }

    private void Awake()
    {
        RowLinkedList = new LinkedList<PlatformRow>();
        Vector3 rowPosX = (1 - width) * 0.5f * TF.right;
        for (int c = 0; c < height; c++)
        {
            Vector3 rowPosZ = c * TF.forward;
            PlatformRow row = Instantiate(
                rowPrefab,
                TF.position + rowPosX + rowPosZ,
                Quaternion.identity,
                TF
            );
            RowLinkedList.AddLast(row);
            row.OnInit(width);
            row.Floating(OnARowFloatingToEndPoint);
        }
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
