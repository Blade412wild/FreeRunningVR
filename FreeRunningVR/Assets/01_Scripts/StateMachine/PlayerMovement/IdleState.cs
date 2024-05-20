using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private float horizontalInput;
    private float verticalInput;
    public override void OnEnter()
    {
        InputManager.Instance.playerInputActions.Idle.Enable();
        Debug.Log("entered Idle");
    }

    public override void OnExit()
    {
        InputManager.Instance.playerInputActions.Idle.Disable();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        if (IsThereInput())
        {
            Controller.SwitchState(typeof(WalkingState));
        }
    }
    private bool IsThereInput()
    {
        Vector2 stickInput = InputManager.Instance.playerInputActions.Idle.MoveVR.ReadValue<Vector2>();
        horizontalInput = stickInput.x;
        verticalInput = stickInput.y;

        if(horizontalInput != 0 ||  verticalInput != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
