using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public event Action OnInsertName;
    public LevelManager levelManager;
    public bool save = false;

    [SerializeField] private string Name;
    [SerializeField] private string fileName;
    [SerializeField] private float maxHighScores;
    private string fullPath;
    private bool MayFillInName;

    private List<PlayerDataStruct> oldHighScore;
    private PlayerDataStruct currentPlayerData;
    private List<float> sortedScores;

    private void Start()
    {
        levelManager.OnEndLevel += PlayerIsFinished;
        Keyboard.OnInsertedName += FinalizeHighScore;
    }

    private void Update()
    {
        if (save)
        {
            SaveList();
            save = false;
        }
    }

    private void PlayerIsFinished()
    {
        MayFillInName = false;
        oldHighScore = CreateHighScoreList();
        currentPlayerData = GetPlayerData();
        sortedScores = SortNewScore(oldHighScore, currentPlayerData);


        if (MayFillInName)
        {
            OnInsertName?.Invoke();
        }

    }

    private void FinalizeHighScore(Keyboard keyBoard)
    {
        currentPlayerData._name = keyBoard.name;
        List<PlayerDataStruct> newHighScore = LinkScoreWithPlayerData(oldHighScore, sortedScores, currentPlayerData);
        oldHighScore = newHighScore;
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

    private void SaveList()
    {
        PlayerDataStruct hoi = new PlayerDataStruct();
        hoi._name = "Nathan";
        hoi._time = 18.0f;

        PlayerDataStruct hoi2 = new PlayerDataStruct();
        hoi2._name = "Felice";
        hoi2._time = 38.0f;

        PlayerDataStruct hoi3 = new PlayerDataStruct();
        hoi3._name = "Simon";
        hoi3._time = 40.0f;

        PlayerDataStruct hoi4 = new PlayerDataStruct();
        hoi4._name = "Max";
        hoi4._time = 100.0f;

        PlayerDataStruct hoi5 = new PlayerDataStruct();
        hoi5._name = "Thijs";
        hoi5._time = 5.0f;

        PlayerDataStruct Player1 = new PlayerDataStruct();
        Player1._name = "Player1";
        Player1._time = 1.0f;

        LevelDataStruct levelDataStruct = new LevelDataStruct();
        levelDataStruct._level = 1;
        levelDataStruct._highScores = new List<PlayerDataStruct>
        {
            hoi,
            hoi2,
            hoi3,
            hoi4,
            hoi5
        };

        SortNewScore(levelDataStruct._highScores, Player1);

        //string path = GetPath();
        //StreamWriter streamWriter = new StreamWriter(path, false);
        //streamWriter.WriteLine(JsonUtility.ToJson(levelDataStruct, true));
        //streamWriter.Close();
        //streamWriter.Dispose();

        //LoadList(path);
    }
    private List<PlayerDataStruct> CreateHighScoreList()
    {
        PlayerDataStruct hoi = new PlayerDataStruct();
        hoi._name = "Nathan";
        hoi._time = 18.0f;

        PlayerDataStruct hoi2 = new PlayerDataStruct();
        hoi2._name = "Felice";
        hoi2._time = 38.0f;

        PlayerDataStruct hoi3 = new PlayerDataStruct();
        hoi3._name = "Simon";
        hoi3._time = 40.0f;

        PlayerDataStruct hoi4 = new PlayerDataStruct();
        hoi4._name = "Max";
        hoi4._time = 100.0f;

        PlayerDataStruct hoi5 = new PlayerDataStruct();
        hoi5._name = "Thijs";
        hoi5._time = 5.0f;

        PlayerDataStruct Player1 = new PlayerDataStruct();
        Player1._name = "Player1";
        Player1._time = 1.0f;

        List<PlayerDataStruct> oldHighScoreData = new List<PlayerDataStruct>
        {
            hoi,
            hoi2,
            hoi3,
            hoi4,
            hoi5
        };

        return oldHighScoreData;
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

    private List<float> SortNewScore(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> scores = new List<float>();
        foreach (PlayerDataStruct playerDataStruct in oldHighscoreData)
        {
            scores.Add(playerDataStruct._time);
        }

        scores.Sort();
        bool scoreIsGoodEnough = false;

        // create new HighScore
        List<float> newHighScoreOrder = new List<float>();

        foreach (float score in scores)
        {
            if (currentPlayerData._time < score && scoreIsGoodEnough == false)
            {
                newHighScoreOrder.Add(currentPlayerData._time);
                scoreIsGoodEnough = true;
                MayFillInName = true;
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

        return newHighScoreOrder;
    }

    private List<PlayerDataStruct> LinkScoreWithPlayerData(List<PlayerDataStruct> highScoreList, List<float> scores, PlayerDataStruct currentPlayerData)
    {
        List<PlayerDataStruct> newHighScoreList = new List<PlayerDataStruct>();
        foreach (float score in scores)
        {
            foreach (PlayerDataStruct playerDataStruct in highScoreList)
            {
                if (score == playerDataStruct._time)
                {
                    newHighScoreList.Add(playerDataStruct);
                    Debug.Log(playerDataStruct._name + " | Time : " + playerDataStruct._time);
                    break;
                }
                else if (score == currentPlayerData._time)
                {
                    newHighScoreList.Add(currentPlayerData);
                    Debug.Log(currentPlayerData._name + " | Time : " + currentPlayerData._time);
                    break;
                }
            }
        }

        return newHighScoreList;
    }
}

[Serializable]
public struct PlayerDataStruct
{
    public string _name;
    public float _time;
}

[Serializable]
public struct LevelDataStruct
{
    public int _level;
    public List<PlayerDataStruct> _highScores;
}

