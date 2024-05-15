using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpState : State
{
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;

    private Rigidbody rb;

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");

    }
    public override void OnEnter()
    {
        Debug.Log("Entered BigJump");
        rb = playerData.playerGameObjects.bodyRB;
        rb.AddForce(Vector3.up * 60.0f, ForceMode.Impulse);
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }
}
