using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MusicData musicData;
    [SerializeField] private List<AudioClip> ActiveAudioClips;
    private PlayerData playerData;
    private AudioSource playerAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        levelManager.OnBeginLevel += StartLevel;
        levelManager.OnEndLevel += FinishLevel;
        gameManager.OnSpawnPlayerDone += SetPlayerData;
    }

    private void SetPlayerData()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        playerAudioSource = playerData.playerGameObjects.audioSource;
    }


    private void StartLevel()
    {
        playerAudioSource.clip = musicData.levelMusic;
        playerAudioSource.Play();
    }

    private void FinishLevel()
    {
        playerAudioSource.Stop();
    }
}
