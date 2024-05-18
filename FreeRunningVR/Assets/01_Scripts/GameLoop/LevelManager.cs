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
    private GameObject playerPrefab;
    private bool MayRunTimer;

    private void Start()
    {
        PlayerGameStopWatch = new StopWatch();
        playerPrefab = gameManager.ObjectData.Read<PlayerData>("playerData").PlayerPrefab;
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
        MayRunTimer = true;
    }

    private void FinishLevel()
    {
        MayRunTimer = false;
        SetPlayerPos();
    }

    private void SetPlayerPos()
    {
        player.position = beginPos.position;
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, beginPos.position, Quaternion.identity);
    }





}
