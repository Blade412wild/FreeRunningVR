using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateHandler : MonoBehaviour
{
    public event Action OnPlayerSpawnDone;
    public GameManager gameManager { get; private set; }
    [SerializeField] private PlayerColliders playerColliders;
    [SerializeField] private PhysicsRig PhysicsRig;
    public StateMachine stateMachine;
    Timer1 playerDone;
    private bool mayUpdate = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.playerData.playerGameObjects = GetComponent<PlayerGameObjects>();
        gameManager.playerData.Colliders = playerColliders.colliders;
        gameManager.playerData.PhysicsRig = PhysicsRig;
        gameManager.PlayerStateHandler = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        playerDone = new Timer1(1);
        playerDone.OnTimerIsDone += PlayerIsDone;
        stateMachine = new StateMachine(this, GetComponents<State>());
        stateMachine.SwitchState(typeof(IdleState));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDone != null && mayUpdate == true)
        {
            playerDone.OnUpdate();      
        }

        stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine?.OnFixedUpdate();
    }

    private void PlayerIsDone()
    {
        Debug.Log("playerIsDone");
        gameManager.PlayerSpawnDone();
        mayUpdate = false;
    }
}
