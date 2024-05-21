using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public LevelManager levelManager;
    public bool save = false;
    [SerializeField] private string Name;
    [SerializeField] private string fileName;
    [SerializeField] private float maxHighScores;
    private string fullPath;

    private void Start()
    {
        levelManager.OnEndLevel += TryToSavePlayerData;
    }

    private void Update()
    {
        if (save)
        {
            SaveList();
            save = false;
        }
    }


    public void TryToSavePlayerData()
    {
        Debug.Log(" try to Save");
        string path = GetPath();
        //checkIfThere is room on the highscore board else return;
        CheckIfThereIsAFile(path);

        PlayerDataStruct playerData = GetPlayerData();
        SaveData(playerData, path);
        CheckIfThereIsAFile(path);

    }


    public string GetPath()
    {
        if (Application.isPlaying)
        {
            return fullPath = Application.dataPath + fileName + ".txt";
        }
        else
        {
            return fullPath = Application.persistentDataPath + fileName + ".txt";
        }
    }

    private void SaveData(PlayerDataStruct playerData, string path)
    {
        StreamWriter streamWriter = new StreamWriter(path, false);
        streamWriter.WriteLine(JsonUtility.ToJson(playerData, true));
        streamWriter.Close();
        streamWriter.Dispose();
    }

    private PlayerDataStruct GetPlayerData()
    {
        PlayerDataStruct playerData = new PlayerDataStruct();
        playerData._name = Name;
        playerData._time = levelManager.PlayerGameStopWatch.currentTime;
        playerData._rank = 1;

        return playerData;
    }

    private void CheckIfThereIsAFile(string path)
    {
        if (File.Exists(path) == false) return;
        StreamReader reader = new StreamReader(path);
        PlayerDataStruct playerData = JsonUtility.FromJson<PlayerDataStruct>(reader.ReadToEnd());
        reader.Close();
        reader.Dispose();
        Debug.Log(" Name : " + playerData._name + " | Time : " + playerData._time);
    }

    private void CheckHighScoreBoard()
    {

    }

    private void SaveList()
    {
        PlayerDataStruct hoi = new PlayerDataStruct();
        hoi._name = "Nathan";
        hoi._time = 18.0f;
        hoi._rank = 1;

        PlayerDataStruct hoi2 = new PlayerDataStruct();
        hoi2._name = "Felice";
        hoi2._time = 38.0f;
        hoi2._rank = 2;

        PlayerDataStruct hoi3 = new PlayerDataStruct();
        hoi2._name = "Simon";
        hoi2._time = 40.0f;
        hoi2._rank = 3;

        LevelDataStruct levelDataStruct = new LevelDataStruct();
        levelDataStruct._level = 1;
        levelDataStruct._highScores = new List<PlayerDataStruct>
        {
            hoi,
            hoi2,
            hoi
        };

        string path = GetPath();
        StreamWriter streamWriter = new StreamWriter(path, false);
        streamWriter.WriteLine(JsonUtility.ToJson(levelDataStruct, true));
        streamWriter.Close();
        streamWriter.Dispose();

        LoadList(path);
    }

    private void LoadList(string path)
    {
        if (File.Exists(path) == false) return;
        StreamReader reader = new StreamReader(path);
        LevelDataStruct levelData = JsonUtility.FromJson<LevelDataStruct>(reader.ReadToEnd());
        reader.Close();
        reader.Dispose();
        Debug.Log(levelData);
    }

    private void CheckHighScoreBoard(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> scores = new List<float>();
        foreach (PlayerDataStruct PlayerDataStruct in oldHighscoreData)
        {
            scores.Add(PlayerDataStruct._time);
        }

        scores.Sort();
        bool scoreIsGoodEnough = false;

        // create new HighScore
        List<float> newHighScoreOrder = new List<float>();

        foreach (float score in scores)
        {
            if (currentPlayerData._time > score && scoreIsGoodEnough == false)
            {
                newHighScoreOrder.Add(currentPlayerData._time);
                scoreIsGoodEnough = true;
            }
            else
            {
                newHighScoreOrder.Add(score);
            }
        }

        newHighScoreOrder.Sort();

        // update de groote van de highscores
        if (newHighScoreOrder.Count >= maxHighScores && scoreIsGoodEnough)
        {
            newHighScoreOrder.Remove(newHighScoreOrder[newHighScoreOrder.Count - 1]);
        }

        CreateNewHighScore(oldHighscoreData, newHighScoreOrder);
    }

    private void CreateNewHighScore(List<PlayerDataStruct> playerDataStructs, List<float> scores)
    {

        foreach(float score in scores)
        {

        }
    }




}

[Serializable]
public struct PlayerDataStruct
{
    public string _name;
    public float _time;
    public int _rank;
}

[Serializable]
public struct LevelDataStruct
{
    public int _level;
    public List<PlayerDataStruct> _highScores;
}

