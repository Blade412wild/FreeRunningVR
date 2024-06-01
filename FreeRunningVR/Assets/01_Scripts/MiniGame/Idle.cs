using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State2<MiniGameManager>
{
    public Idle(MiniGameManager owner) : base(owner)
    {
    }
    public override void OnEnter()
    {
        Debug.Log(" entered Idle");
    }
    public override void OnExit() { }
    public override void OnUpdate() { }
}