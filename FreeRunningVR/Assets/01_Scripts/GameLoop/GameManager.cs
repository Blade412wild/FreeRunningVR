using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnSpawnPlayer;
    public Scratchpad ObjectData { get; private set; }
    public PlayerData playerData;
    [SerializeField] private PlayerGameObjects playerGameObjects;
    private StateMachine GameStateMachine;

    private void Awake()
    {
        ObjectData = new Scratchpad();
        ObjectData.Write("playerData", playerData);
        playerData.playerGameObjects = playerGameObjects;

    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateMachine = new StateMachine(this);
        OnSpawnPlayer?.Invoke();
    }
}
