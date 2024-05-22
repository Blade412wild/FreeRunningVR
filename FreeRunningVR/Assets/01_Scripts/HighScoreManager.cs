using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public event Action<List<PlayerDataStruct>> OnHighScoreDataIsDone;
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
            //SaveList();
            save = false;
        }
    }

    private void PlayerIsFinished()
    {
        MayFillInName = false;
        currentPlayerData = GetPlayerData();

        string path = GetPath();
        LevelDataStruct? highScoreFile = LoadList(path);

        if (highScoreFile == null)
        {
            highScoreFile = CreateNewHighScore();
            //highScoreFile.Value._highScores.Add(currentPlayerData);

            oldHighScore = highScoreFile.Value._highScores;
            sortedScores = new List<float> { currentPlayerData._time };
            MayFillInName = true;

        }
        else
        {
            oldHighScore = highScoreFile.Value._highScores;
            sortedScores = SortNewScoreNew(highScoreFile.Value._highScores, currentPlayerData);
            MayFillInName = CheckIfPlayerMayFillInName(sortedScores, currentPlayerData);
        }


        if (MayFillInName)
        {
            OnInsertName?.Invoke();
        }
    }

    private bool CheckIfPlayerMayFillInName(List<float> sortedScores, PlayerDataStruct currentPlayerData)
    {
        foreach(float score in sortedScores)
        {
            if(score == currentPlayerData._time)
            {
                return true;
            }
        }

        return false;
    }

    private LevelDataStruct CreateNewHighScore()
    {
        LevelDataStruct newlevelDataStruct = new LevelDataStruct();
        newlevelDataStruct._level = 1;
        newlevelDataStruct._highScores = new List<PlayerDataStruct>();
        return newlevelDataStruct;
    }

    private void FinalizeHighScore(Keyboard keyBoard)
    {
        currentPlayerData._name = keyBoard.name;
        List<PlayerDataStruct> newHighScore = LinkScoreWithPlayerData(oldHighScore, sortedScores, currentPlayerData);
        //oldHighScore = newHighScore;
        OnHighScoreDataIsDone?.Invoke(newHighScore);
        string path = GetPath();
        SaveList(path, newHighScore);
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

    private void SaveList(string path, List<PlayerDataStruct> newHighScores)
    {
        LevelDataStruct newlevelDataStruct = new LevelDataStruct();
        newlevelDataStruct._level = 1;
        newlevelDataStruct._highScores = newHighScores;
        {
        };

        StreamWriter streamWriter = new StreamWriter(path, false);
        streamWriter.WriteLine(JsonUtility.ToJson(newlevelDataStruct, true));
        streamWriter.Close();
        streamWriter.Dispose();
    }
    private List<PlayerDataStruct> CreateHighScoreList()
    {

        List<PlayerDataStruct> oldHighScoreData = new List<PlayerDataStruct>();

        return oldHighScoreData;
    }
    private LevelDataStruct? LoadList(string path)
    {
        if (File.Exists(path) == false) return null;
        StreamReader reader = new StreamReader(path);
        LevelDataStruct levelHighScoreData = JsonUtility.FromJson<LevelDataStruct>(reader.ReadToEnd());
        reader.Close();
        reader.Dispose();
        Debug.Log(levelHighScoreData);
        return levelHighScoreData;
    }

    private List<float> SortNewScoreold(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {

        // seperate the score;
        List<float> scores = new List<float>();
        foreach (PlayerDataStruct playerDataStruct in oldHighscoreData)
        {
            scores.Add(playerDataStruct._time);
        }

        scores.Sort();
        bool scoreIsGoodEnough = false;
        bool reLoop = false;

        // create new HighScore
        List<float> newHighScoreOrder = new List<float>();

        for (int i = 0; i < scores.Count; i++)
        {
            if (currentPlayerData._time > scores[i] && scoreIsGoodEnough == false)
            {
                newHighScoreOrder.Add(scores[i]);
                scoreIsGoodEnough = true;

            }


            if (currentPlayerData._time < scores[i] && scoreIsGoodEnough == false)
            {
                newHighScoreOrder.Add(currentPlayerData._time);
                scoreIsGoodEnough = true;
                MayFillInName = true;
                reLoop = true;
            }

            if (reLoop == true)
            {
                i--;
                reLoop = false;
            }
        }

        newHighScoreOrder.Sort();

        // update de groote van de highscores
        if (newHighScoreOrder.Count > maxHighScores && scoreIsGoodEnough)
        {
            newHighScoreOrder.Remove(newHighScoreOrder[newHighScoreOrder.Count - 1]);
        }

        return newHighScoreOrder;
    }
    private List<float> SortNewScore(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> scores = new List<float>();

        if (oldHighscoreData.Count < maxHighScores)
        {
            oldHighscoreData.Add(currentPlayerData);
        }

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
    private List<float> SortNewScoreNew(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> scores = new List<float>();

        scores.Add(currentPlayerData._time);
        foreach (PlayerDataStruct playerDataStruct in oldHighscoreData)
        {
            scores.Add(playerDataStruct._time);
        }

        scores.Sort();

        while (scores.Count > maxHighScores)
        {
            scores.Remove(scores[scores.Count - 1]);
        }

        return scores;
    }
    private List<float> SortNewScore2(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
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
        if (highScoreList.Count == 0)
        {
            newHighScoreList.Add(currentPlayerData);
            return newHighScoreList;
        }

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

