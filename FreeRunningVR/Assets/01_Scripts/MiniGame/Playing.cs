using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : State2<MiniGameManager>
{
    public Playing(MiniGameManager owner ) : base(owner)
    {
    }
    public override void OnEnter()
    {
        Debug.Log(" entered Playing");
    }
    public override void OnExit() { }
    public override void OnUpdate() { }
}
