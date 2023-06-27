using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABSStateMachine<T>: GameUnit where T : ABSStateMachine<T>
{
    [Header("State Machine:")]
    public string currentStateLog;
    public ABSState<T> CurrentState { get; protected set; }
    private void Awake() => OnInit();
    protected virtual void Update()
    {
        if (IsActivating())
        {
            CurrentState.OnExecute();
        }
    }
    protected virtual void OnInit()
    {
        InitStates();
    }
    public virtual void ChangeState(ABSState<T> state)
    {
        if (CurrentState != state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
            currentStateLog = CurrentState.ToString();
        }
    }
    protected abstract bool IsActivating();
    protected abstract void InitStates();
}
