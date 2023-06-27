using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABSState <T>
{
    protected T unit;
    public ABSState(T unit) => this.unit = unit;
    public abstract void OnEnter();
    public abstract void OnExecute();
    public abstract void OnExit();
}
