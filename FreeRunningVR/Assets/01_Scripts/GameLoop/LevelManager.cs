using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StopWatch PlayerGameStopWatch { get; private set; }
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FinishLevel finishLevel;
    [SerializeField] private StartLevel startLevel;
    [SerializeField] private Transform player;
    [SerializeField] private Transform beginPos;
    private PlayerData playerData;
    private GameObject playerPrefab;
    private bool MayRunTimer;

    private void Start()
    {
        PlayerGameStopWatch = new StopWatch();
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        playerPrefab = playerData.PlayerPrefab;
        startLevel = FindObjectOfType<StartLevel>();
        finishLevel = FindObjectOfType<FinishLevel>();

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
        Debug.Log("begin Level");
        MayRunTimer = true;
    }

    private void FinishLevel()
    {
        MayRunTimer = false;
        SetPlayerPos(beginPos.position);
    }

    private void SetPlayerPos(Vector3 targetPos)
    {
        player.position = targetPos;
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player = playerData.playerGameObjects.orientation.parent.parent;
        SetPlayerPos(beginPos.position);
    }




}
