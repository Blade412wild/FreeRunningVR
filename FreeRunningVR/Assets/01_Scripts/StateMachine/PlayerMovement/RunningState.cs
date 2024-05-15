using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;

    int count;

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");

    }
    public override void OnEnter()
    {
        Debug.Log("entered Running");
    }

    public override void OnExit()
    {
        Debug.Log("Exited Running");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        //count += 1;
        //Debug.Log("count : " +  count);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Controller.SwitchState(typeof(BigJumpState));
        }
    }
}
