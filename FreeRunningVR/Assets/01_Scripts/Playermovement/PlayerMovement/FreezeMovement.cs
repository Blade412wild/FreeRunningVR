using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement : State
{
    public override void OnEnter()
    {
        Debug.Log("entered Freeze");
    }

    public override void OnExit()
    {
        Debug.Log("exit Freeze");

    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}
