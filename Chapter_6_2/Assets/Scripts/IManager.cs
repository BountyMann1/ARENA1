using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    string State { get; set; }
    void Initialize();
}
public abstract class BaseManager
{
    protected string _state = "Manager is not initialized...";
    public abstract string State { get; set; }
    public abstract void Initialize();
}
public class CombatManager : BaseManager
{
    public override string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public override void Initialize()
    {
        _state = "Combat Manager initialized...";
        Debug.Log(_state);
    }
}
