using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action OnBeginLevel;
    public event Action OnEndLevel;
    public event Action OnDataInputDone;
    public StopWatch PlayerGameStopWatch { get; private set; }
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private FinishLevel finishLevel;
    [SerializeField] private StartLevel startLevel;
    [SerializeField] private Transform player;
    [SerializeField] private Transform beginPos;
    [SerializeField] private float finishTimerDuration = 5.0f;

    private PlayerData playerData;
    private GameObject playerPrefab;
    private bool MayRunTimer;
    private Timer1 finishTimer;
    private GameObject test;

   

    private void Start()
    {
        PlayerGameStopWatch = new StopWatch();
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        playerPrefab = playerData.PlayerPrefab;
        SetGameLevelData();

        highScoreManager.OnRestartLevel += ResetLevel;
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
        test = Instantiate(playerPrefab, beginPos.position, Quaternion.identity);
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
        playerData.playerGameObjects.rightHandRB.isKinematic = true;
        playerData.playerGameObjects.leftHandRB.isKinematic = true;


        playerData.playerGameObjects.rightHandRB.position = targetPos;
        playerData.playerGameObjects.leftHandRB.position = targetPos;
        player.position = targetPos;

        playerData.playerGameObjects.rightHandRB.isKinematic = false;
        playerData.playerGameObjects.leftHandRB.isKinematic = false;


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

    private void ResetLevel()
    {
        Debug.Log("ResetLevel");
        MayRunTimer = false;
        PlayerGameStopWatch.ResetTimer();
        OnDataInputDone?.Invoke();
    }
}
