using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateHandler : MonoBehaviour
{
    public event Action OnPlayerSpawnDone;
    public GameManager gameManager { get; private set; }
    [SerializeField] private PlayerColliders playerColliders;
    public StateMachine stateMachine;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.playerData.playerGameObjects = GetComponent<PlayerGameObjects>();
        gameManager.playerData.Colliders = playerColliders.colliders;
        gameManager.PlayerSpawnDone();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine(this, GetComponents<State>());
        stateMachine.SwitchState(typeof(IdleState));
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine?.OnUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine?.OnFixedUpdate();

    }
}
