using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    public GameManager gameManager { get; private set; }
    public StateMachine stateMachine;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.playerData.playerGameObjects = GetComponent<PlayerGameObjects>();
        Debug.Log(gameManager);
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
