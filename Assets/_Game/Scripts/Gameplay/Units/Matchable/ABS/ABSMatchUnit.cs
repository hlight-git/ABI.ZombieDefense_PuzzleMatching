using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ABSMatchUnit : GameUnit
{
    MatchCell cell;
    public bool IsDead { get; private set; }
    public bool CanMatch { get; protected set; }
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
    public virtual void OnDespawn()
    {
        UnLinkToCell();
        SimplePool.Despawn(this);
    }

    protected virtual void OnDeath()
    {
        IsDead = true;
        OnDespawn();
    }
    public abstract void OnInit(MatchUnitFactory factory);
    public abstract void OnMatched(int matchValue);
    public abstract void OnTakeDamage(float damage);
}

public abstract class ABSMatchUnit<UnitStatsT, FactoryT> : ABSMatchUnit
    where UnitStatsT : UnitStats<UnitStatsT>, new()
    where FactoryT : MatchUnitFactory
{
    UnitStatsT statsContainer;

    protected FactoryT factory;
    protected UnitStatsT generalStats;
    protected UnitStatsT buffStats;

    protected float healthPoint;
    public UnitStatsT Stats
    {
        get
        {
            statsContainer.Combine(generalStats, buffStats);
            return statsContainer;
        }
    }
    public override void OnInit(MatchUnitFactory factory)
    {
        this.factory = (FactoryT)factory;
        statsContainer = new UnitStatsT();
    }
    public override void OnTakeDamage(float damage)
    {
        healthPoint -= damage;
        if (healthPoint < 0)
        {
            OnDeath();
        }
    }
}