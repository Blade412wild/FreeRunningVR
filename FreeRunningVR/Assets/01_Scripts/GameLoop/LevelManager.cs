using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public event Action OnBeginLevel;
    public event Action OnEndLevel;
    public StopWatch PlayerGameStopWatch { get; private set; }
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FinishLevel finishLevel;
    [SerializeField] private StartLevel startLevel;
    [SerializeField] private Transform player;
    [SerializeField] private Transform beginPos;
    [SerializeField] private float finishTimerDuration = 5.0f;
    private PlayerData playerData;
    private GameObject playerPrefab;
    private bool MayRunTimer;
    private Timer1 finishTimer;



    private void Start()
    {
        PlayerGameStopWatch = new StopWatch();
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        playerPrefab = playerData.PlayerPrefab;
        SetGameLevelData();

        gameManager.OnSpawnPlayer += SpawnPlayer;
        startLevel.OnStartLevel += BeginLevel;
        finishLevel.OnFinishLevel += FinishLevel;
    }

    private void Update()
    {
        if (MayRunTimer)
        {
            PlayerGameStopWatch.OnUpdate();
        }
    }

    private void BeginLevel()
    {
        OnBeginLevel?.Invoke();
        Debug.Log("begin Level");
        MayRunTimer = true;
    }

    private void FinishLevel()
    {
        MayRunTimer = false;
        SetPlayerBackToHome();
        OnEndLevel?.Invoke();
    }

    //private void SetPlayerPos(Vector3 targetPos)
    //{
    //    player.position = targetPos;
    //}

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, beginPos.position, Quaternion.identity);
        player = playerData.playerGameObjects.orientation.parent.parent;
        //SetPlayerPos(beginPos.position);
    }

    private void SetPlayerBackToHome()
    {
        SetPlayerPos(beginPos.position);
    }

    private void SetGameLevelData()
    {
        startLevel = FindObjectOfType<StartLevel>();
        finishLevel = FindObjectOfType<FinishLevel>();

        gameManager.ObjectData.Write("levelData", levelData);
        levelData.startLevel = startLevel;
        levelData.finishLevel = finishLevel;
    }

    private void SetPlayerPos(Vector3 targetPos)
    {
        foreach(Collider collider in playerData.Colliders)
        {

        }

        player.position = targetPos;

        ResetRigidBody(playerData.playerGameObjects.bodyRB);
        ResetRigidBody(playerData.playerGameObjects.headRB);
        ResetRigidBody(playerData.playerGameObjects.rightHandRB);
        ResetRigidBody(playerData.playerGameObjects.leftHandRB);
    }

    private void ResetRigidBody(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
