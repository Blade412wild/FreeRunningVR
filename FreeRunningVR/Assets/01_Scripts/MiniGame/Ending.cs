using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : State2<MiniGameManager>
{
    public Ending(MiniGameManager owner) : base(owner)
    {
    }
    public override void OnEnter()
    {
        Debug.Log(" entered Idle");
    }
    public override void OnExit() { }
    public override void OnUpdate() { }
}
