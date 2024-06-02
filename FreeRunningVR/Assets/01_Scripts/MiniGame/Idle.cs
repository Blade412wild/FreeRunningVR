using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Idle : State2<MiniGameManager>
{
    private TargetManager2 targetManager;

    public Idle(MiniGameManager owner, TargetManager2 targetManager) : base(owner)
    {
        this.targetManager = targetManager;
    }
    public override void OnEnter()
    {
        Debug.Log(" entered Idle");
        targetManager.BuildLevel();
    }
    public override void OnExit() { }
    public override void OnUpdate()
    {
    }
}
