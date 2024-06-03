using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] private PlayerStateHandler playerStateHandler;
    private GameManager gameManager;
    PlayerData playerData;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        gameManager = playerStateHandler.gameManager;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
    }
    public override void OnEnter()
    {
        InputManager.Instance.playerInputActions.Idle.Enable();
        Debug.Log("entered Idle");
    }

    public override void OnExit()
    {
        InputManager.Instance.playerInputActions.Idle.Disable();
        //playerData.playerGameObjects.leftHandRB.mass = 0.1f;
        //playerData.playerGameObjects.leftHandRB.velocity = Vector3.zero;
        //playerData.playerGameObjects.leftHandRB.angularVelocity = Vector3.zero;

        //playerData.playerGameObjects.rightHandRB.mass = 0.1f;
        //playerData.playerGameObjects.rightHandRB.velocity = Vector3.zero;
        //playerData.playerGameObjects.rightHandRB.angularVelocity = Vector3.zero;
        playerData.playerheight = playerData.playerGameObjects.bodyCollider.height;
        playerData.PreviousHeight = playerData.playerheight;
        Debug.Log(" player Height = " +  playerData.playerheight);
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
