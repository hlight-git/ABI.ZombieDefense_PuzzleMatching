using DG.Tweening;
using HlightSDK;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MatchBoard : GameUnit
{
    [SerializeField] MatchUnitGrabber matchUnitGrabber;

    int width;
    int height;
    LinkedListNode<PlatformRow> nextRowNode;
    List<MatchCell[]> cells;
    List<Task> slideTasks;
    Dictionary<PoolType, UnitStats> statsCache;
    Dictionary<MatchUnitType, MatchUnitFactory> unitFactories;

    public bool IsMovable => slideTasks.Count == 0;
    public Task WhenAllSlideTasks => Task.WhenAll(slideTasks);

    public void OnInit(PlatformBoard platformBoard, int width, int height)
    {
        this.width = width;
        this.height = height;
        nextRowNode = platformBoard.RowLinkedList.First;

        cells = new List<MatchCell[]>();
        slideTasks = new List<Task>();
        statsCache = new Dictionary<PoolType, UnitStats>();
        InitFactories();

        for (int i = 0; i < height; i++)
        {
            AddRow();
        }
        Fill();
        matchUnitGrabber.enabled = true;
    }
    void InitFactories()
    {
        unitFactories = new Dictionary<MatchUnitType, MatchUnitFactory>();

        for (int i = 0; i < Constant.SELECT_MU_SLOT; i++)
        {
            MatchUnitData unitData = UserData.Ins.MatchUnitsInUse[i];
            switch (unitData.Type)
            {
                case MatchType.CollectUnit:
                    unitFactories.Add(unitData.UnitType, new CollectUnitFactory(unitData));
                    break;
                case MatchType.Hero:
                    unitFactories.Add(unitData.UnitType, new HeroFactory(unitData));
                    break;
            }
        }
        unitFactories.Add(MatchUnitType.Bonus, new CollectUnitFactory(new CollectUnitData(MatchUnitType.Bonus)));
    }
    public void AddRow()
    {
        cells.Add(new MatchCell[width]);
        int curRow = cells.Count - 1;
        PlatformRow row = nextRowNode.Value;
        for (int curCol = 0; curCol < width; curCol++)
        {
            cells[curRow][curCol] = new MatchCell(curRow, curCol, row.Tiles[curCol]);
        }
        nextRowNode = nextRowNode.Next;
    }

    ABSMatchUnit CreateUnit(Vector3 position)
    {
        MatchUnitFactory factory = unitFactories.Values.Choice();
        return factory.CreateUnit(position, Quaternion.identity, TF);
    }

    void SlideUnitToCell(ABSMatchUnit unit, MatchCell cell)
    {
        slideTasks.Add(unit.SlideToCell(cell, OnCompleteAction: OnAnUnitFinishedSliding));
    }

    void OnAnUnitFinishedSliding()
    {
        slideTasks.RemoveAt(0);
    }

    void Swap(ABSMatchUnit unitA, ABSMatchUnit unitB)
    {
        MatchCell cellA = unitA.Cell;
        MatchCell cellB = unitB.Cell;
        SlideUnitToCell(unitA, cellB);
        SlideUnitToCell(unitB, cellA);
        unitA.Cell = cellB;
        unitB.Cell = cellA;
    }

    MatchCell GetNeighborCell(MatchCell cell, MatchDir matchDir)
    {
        Vector2 matchDirVector = MatchDirAsVector(matchDir);
        int resRow = Mathf.RoundToInt(cell.Row + matchDirVector.y);
        int resCol = Mathf.RoundToInt(cell.Col + matchDirVector.x);
        if (
            0 <= resRow && resRow < height &&
            0 <= resCol && resCol < width
        )
        {
            return cells[resRow][resCol];
        }
        return null;
    }

    MatchDir? RoundMoveDirection(Vector2 direction)
    {
        for (int i = 0; i < matchDirs.Count; i++)
        {
            MatchDir matchDir = matchDirs[i];
            if (
                Vector2.Angle(direction, MatchDirAsVector(matchDir)) <
                Constant.MatchUnit.MAX_ANGLE_OFFSET_ALLOWED_TO_MOVE)
            {
                return matchDir;
            }
        }
        return null;
    }
    bool TryMove(ABSMatchUnit unit, MatchDir matchDir)
    {
        MatchCell destinationCell = GetNeighborCell(unit.Cell, matchDir);
        if (destinationCell != null)
        {
            Swap(unit, destinationCell.Unit);
            return true;
        }
        return false;
    }

    void CheckMatch(int irow, int icol, int rightCheckBit, int backwardCheckBit, int rightCheckedVal, int backwardCheckedVal, ref int[,] matchVal)
    {
        MatchCell curCell = cells[irow][icol];
        if (curCell.Unit.CanMatch)
        {
            bool isRightChecked = (matchVal[irow, icol] >> rightCheckBit) % 2 == 1;
            bool isBackwardChecked = (matchVal[irow, icol] >> backwardCheckBit) % 2 == 1;
            if (!isRightChecked)
            {
                CheckMatchSequence(curCell, MatchDir.Right, rightCheckedVal, ref matchVal);
            }
            if (!isBackwardChecked)
            {
                CheckMatchSequence(curCell, MatchDir.Backward, backwardCheckedVal, ref matchVal);
            }
        }
    }
    void CheckMatchSequence(MatchCell startCell, MatchDir dir, int checkedVal, ref int[,] resType)
    {
        List<MatchCell> seq = GetSameTypeSequence(startCell, dir);
        resType[startCell.Row, startCell.Col] += checkedVal;
        if (seq.Count > 2)
        {
            // Đánh dấu đã duyệt các quân và sẽ ăn chúng
            for (int i = 0; i < seq.Count; i++)
            {
                resType[seq[i].Row, seq[i].Col] += checkedVal + 2;
            }

            // Đánh dấu quân ở 2 đầu
            int iFirstCell = 0;
            int iLastCell = seq.Count - 1;
            resType[seq[iFirstCell].Row, seq[iFirstCell].Col]--;
            resType[seq[iLastCell].Row, seq[iLastCell].Col]--;

            // Trừ bỏ giá trị đánh dấu đã duyệt được tính 2 lần
            resType[startCell.Row, startCell.Col] -= checkedVal;
        }
    }
    List<MatchCell> GetSameTypeSequence(MatchCell startCell, MatchDir dir)
    {
        List<MatchCell> res = new List<MatchCell>();
        MatchCell entry = startCell;
        while (IsContainsSameUnitType(startCell, entry))
        {
            res.Add(entry);
            entry = GetNeighborCell(entry, dir);
        }
        return res;
    }
    bool IsContainsSameUnitType(MatchCell hostCell, MatchCell compareCell) => compareCell != null && hostCell.UnitType == compareCell.UnitType;
    public void Fill()
    {
        // Xét lần lượt các cột, ở mỗi cột lại xét lần lượt từng hàng từ trên xuống dưới
        for (int icol = 0; icol < width; icol++)
        {
            int nullCount = 0;
            for (int irow = height - 1; irow >= 0; irow--)
            {
                // Nếu gặp ô trống thì tăng biến đếm, còn không thì di chuyển unit ở ô này lên ô trống cũ nhất
                ABSMatchUnit unit = cells[irow][icol].Unit;
                if (unit == null)
                {
                    nullCount++;
                }
                else
                {
                    MatchCell targetCell = cells[irow + nullCount][icol];
                    SlideUnitToCell(unit, targetCell);
                    unit.Cell = targetCell;
                }
            }
            // Sinh ra số lượng unit mới bằng với số lượng ô trống đã đếm để lấp cột
            Vector3 newUnitPosX = (icol - (width - 1) * 0.5f) * TF.right;
            Vector3 newUnitPosY = TF.up;
            for (int i = nullCount; i > 0; i--)
            {
                Vector3 newUnitPosZ = -i * TF.forward;
                ABSMatchUnit unit = CreateUnit(newUnitPosX + newUnitPosY + newUnitPosZ);
                MatchCell cell = cells[nullCount - i][icol];
                SlideUnitToCell(unit, cell);
                unit.Cell = cell;
            }
        }
        CheckMatch();
    }
    public void TryMove(ABSMatchUnit unit, Vector2 direction)
    {
        MatchDir? roundedMoveDirection = RoundMoveDirection(direction);
        if (roundedMoveDirection != null && TryMove(unit, roundedMoveDirection.Value))
        {
            CheckMatch();
        }
    }
    public async void CheckMatch()
    {
        await WhenAllSlideTasks;

        int[,] matchVal = new int[height, width];
        int rightCheckBit = 9;
        int backwardCheckBit = 10;
        int rightCheckedVal = 1 << rightCheckBit;
        int backwardCheckedVal = 1 << backwardCheckBit;

        for (int irow = height - 1; irow >= 0; irow--)
        {
            for (int icol = 0; icol < width; icol++)
            {
                CheckMatch(irow, icol, rightCheckBit, backwardCheckBit, rightCheckedVal, backwardCheckedVal, ref matchVal);
            }
        }

        bool hadMatched = false;
        for (int irow = height - 1; irow >= 0; irow--)
        {
            for (int icol = 0; icol < width; icol++)
            {
                MatchCell curCell = cells[irow][icol];
                int curMatchVal = matchVal[irow, icol] - rightCheckedVal - backwardCheckedVal;
                if (curMatchVal > 0)
                {
                    hadMatched = true;
                    curCell.Unit.OnMatched(curMatchVal);
                }
            }
        }
        if (hadMatched)
        {
            Fill();
        }
    }
}

public partial class MatchBoard
{
    enum MatchDir { Right, Forward, Left, Backward }
    readonly List<MatchDir> matchDirs = Util.Enum.ToList<MatchDir>();

    Vector2 MatchDirAsVector(MatchDir matchDir)
    {
        return matchDir switch
        {
            MatchDir.Right => Vector2.right,
            MatchDir.Forward => Vector2.up,
            MatchDir.Left => Vector2.left,
            MatchDir.Backward => Vector2.down,
            _ => Vector2.zero
        };
    }
}
[System.Serializable]
public class MatchCell
{
    PlatformTile tile;
    public ABSMatchUnit Unit { get; set; }
    public int Row { get; private set; }
    public int Col { get; private set; }
    public PoolType UnitType => Unit.poolType;
    public Vector3 Position => tile.StandPosTF.position;
    public MatchCell(int row, int col, PlatformTile tile)
    {
        Row = row;
        Col = col;
        this.tile = tile;
    }
}