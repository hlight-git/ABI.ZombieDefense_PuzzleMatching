using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ABSMatchUnit : GameUnit
{
    MatchCell cell;

    public MatchUnitFactory Factory { get; private set; }
    public MatchCell Cell
    {
        get => cell;
        set
        {
            cell = value;
            cell.Unit = this;
        }
    }

    void UnLinkToCell()
    {
        if (Cell != null)
        {
            Cell.Unit = null;
        }
        cell = null;
    }

    public Task SlideToCell(MatchCell cell, float slideSpeed = Constant.MatchUnit.UNIT_FILL_SLIDE_SPEED, TweenCallback OnCompleteAction = null)
    {
        UnLinkToCell();
        return TF.DOMove(cell.Position, slideSpeed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(OnCompleteAction).AsyncWaitForCompletion();
    }
    public virtual void OnInit(MatchUnitFactory factory)
    {
        Factory = factory;
    }
    public virtual void OnDespawn()
    {
        UnLinkToCell();
        SimplePool.Despawn(this);
    }

    public abstract bool CanMatch { get; }
    public abstract void OnMatched(int matchValue);
}

public abstract class ABSMatchUnit<UnitStatsT> : ABSMatchUnit where UnitStatsT : UnitStats
{
    UnitStatsT stats => Factory.GetStats<UnitStatsT>(poolType);
}