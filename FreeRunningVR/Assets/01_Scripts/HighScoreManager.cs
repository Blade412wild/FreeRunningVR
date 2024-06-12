using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public event Action<List<PlayerDataStruct>> OnHighScoreDataIsDone;
    public event Action OnRestartLevel;
    public event Action OnInsertName;
    public LevelManager levelManager;
    public bool save = false;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerScoreData playerScoreData;
    [SerializeField] private HighscoreRequirements highscoreRequirements;
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private string Name;
    [SerializeField] private string fileName;
    [SerializeField] private float maxHighScores;
    private string fullPath;
    private bool mayFillInName;

    private List<PlayerDataStruct> oldHighScore;
    private List<PlayerDataStruct> newHighScore = new List<PlayerDataStruct>();
    private PlayerDataStruct currentPlayerData;
    private GetPlayerData getPlayerData;
    private List<float> sortedScores;
    private int counterPlayer = 0;
    private string defaultPlayerName = "defaultPlayer";


    private void Start()
    {
        levelManager.OnEndLevel += PlayerIsFinished;
        Keyboard.OnInsertedName += FinalizeHighScore;

        getPlayerData = new GetPlayerData(highscoreRequirements, levelManager, targetManager);
        //LoadHighScoreOnStart();


    }

    private void Update()
    {
        if (save)
        {
            //SaveList();
            save = false;
        }
    }

    private void LoadHighScoreOnStart()
    {
        string path = GetPath();
        LevelDataStruct? highScoreFile = LoadList(path);

        if (highScoreFile != null)
        {
            OnHighScoreDataIsDone?.Invoke(highScoreFile.Value._highScores);
        }
    }

    private void PlayerIsFinished()
    {
        mayFillInName = false;
        counterPlayer++;
        currentPlayerData = GetPlayerData();
        currentPlayerData._scrore.y += counterPlayer;
        currentPlayerData._name = defaultPlayerName;
        //LevelDataStruct highScoreFile = CreateNewHighScore();
        //highScoreFile._highScores = FillInTestOldHighScores();
        //oldHighScore = highScoreFile._highScores;

        string path = GetPath();
        LevelDataStruct? highScoreFile = LoadList(path);

        if (highScoreFile == null)
        {
            highScoreFile = CreateNewHighScore();

            oldHighScore = highScoreFile.Value._highScores;
            newHighScore.Add(currentPlayerData);
            mayFillInName = true;

        }
        else
        {
            oldHighScore = highScoreFile.Value._highScores;
            List<Vector4> allScores = new List<Vector4>();
            List<PlayerDataStruct> allScoresAndPlayer = new List<PlayerDataStruct>();
            allScoresAndPlayer.Add(currentPlayerData);


            Debug.Log("===== all Scores =====");
            // fill in list with all scores
            foreach (PlayerDataStruct playerDataStruct in oldHighScore)
            {
                allScoresAndPlayer.Add(playerDataStruct);
                allScores.Add(playerDataStruct._scrore);
                Debug.Log("player name : " + playerDataStruct._name);
            }

            ////
            /// verander dit naar een lijst van playerDataStructs, zo verlies je geen refrences en is de lijst gelijk gesorteerd
            newHighScore = SortScore2(currentPlayerData, oldHighScore);
            //List<Vector4> sortedList = SortScore(currentPlayerData._scrore, allScores);


            //while (sortedList.Count > maxHighScores)
            //{
            //    sortedList.Remove(sortedList[sortedList.Count - 1]);
            //}

            while (newHighScore.Count > maxHighScores)
            {
                newHighScore.Remove(newHighScore[newHighScore.Count - 1]);
            }

            /////////////////////////////////////////////////////
            ///deze moet weg en ik moet niet de lijst sorteren op vector4s
            //Debug.Log("===== Sorted List =====");
            //foreach (Vector4 score in sortedList)
            //{
            //    bool isplayetAdded = false;
            //    for (int i = 0; i < allScoresAndPlayer.Count; i++)
            //    {
            //        if (score != allScoresAndPlayer[i]._scrore) continue;
            //        Debug.Log(allScoresAndPlayer[i]._name + " : " + allScoresAndPlayer[i]._scrore);

            //        if (score == currentPlayerData._scrore && i < maxHighScores && isplayetAdded != true)
            //        {
            //            mayFillInName = true;
            //            isplayetAdded = true;
            //            newHighScore.Add(allScoresAndPlayer[i]);
            //        }
            //        else if(score != currentPlayerData._scrore && i < maxHighScores)
            //        {
            //            newHighScore.Add(allScoresAndPlayer[i]);
            //        }
            //    }
            //}

            Debug.Log("list count : " + newHighScore.Count);

        }


        if (mayFillInName)
        {
            OnInsertName?.Invoke();
        }
        else
        {
            OnRestartLevel?.Invoke();
        }
    }

    private List<Vector4> StoreRawScore()
    {
        List<Vector4> allScores = new List<Vector4>();
        List<PlayerDataStruct> allScoresAndPlayer = new List<PlayerDataStruct>();
        allScoresAndPlayer.Add(currentPlayerData);


        Debug.Log("===== all Scores =====");
        // fill in list with all scores
        foreach (PlayerDataStruct playerDataStruct in oldHighScore)
        {
            allScoresAndPlayer.Add(playerDataStruct);
            allScores.Add(playerDataStruct._scrore);
            Debug.Log("player name : " + playerDataStruct._name);
        }
        return allScores;
    }
    private List<PlayerDataStruct> SortScore2(PlayerDataStruct playerData, List<PlayerDataStruct> allScores)
    {
        Debug.Log("===============");
        Debug.Log("Sorting Grade");

        List<PlayerDataStruct> sameGradeList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> higherGradeList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> lowerGradeList = new List<PlayerDataStruct>();


        List<PlayerDataStruct> sameTimeList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> higherTimeList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> lowerTimeList = new List<PlayerDataStruct>();


        List<PlayerDataStruct> sameHitList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> higherHitList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> lowerHitList = new List<PlayerDataStruct>();

        List<PlayerDataStruct> sameAccuracyList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> higherAccuracyList = new List<PlayerDataStruct>();
        List<PlayerDataStruct> lowerAccuracyList = new List<PlayerDataStruct>();

        List<PlayerDataStruct> sortedList = new List<PlayerDataStruct>();

        List<PlayerDataStruct>[] ListArray =
        {
            higherGradeList,
            higherTimeList,
            higherHitList,
            higherAccuracyList,
            sameAccuracyList,
            lowerAccuracyList,
            lowerHitList,
            lowerTimeList,
            lowerGradeList
        };

        DateTime startTime = DateTime.Now;
        bool lastnesstle = true;
        bool playerHasBeenAdded = false;


        // check for same Grade 
        for (int i = 0; i < allScores.Count; i++)
        {
            if (allScores[i]._scrore.x == playerData._scrore.x)
            {
                sameGradeList.Add(allScores[i]);
                lastnesstle = false;
            }
            else if (allScores[i]._scrore.x < playerData._scrore.x)
            {
                higherGradeList.Add(allScores[i]);
            }
            else if (allScores[i]._scrore.x > playerData._scrore.x)
            {
                lowerGradeList.Add(allScores[i]);
            }

        }

        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherGradeList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for time 
        for (int i = 0; i < sameGradeList.Count; i++)
        {
            if (sameGradeList[i]._scrore.y == playerData._scrore.y)
            {
                sameTimeList.Add(sameGradeList[i]);
                lastnesstle = false;
            }
            else if (sameGradeList[i]._scrore.y < playerData._scrore.y)
            {
                higherTimeList.Add(sameGradeList[i]);
            }
            else if (sameGradeList[i]._scrore.y > playerData._scrore.y)
            {
                lowerTimeList.Add(sameGradeList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherTimeList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for hits
        for (int i = 0; i < sameTimeList.Count; i++)
        {
            if (sameTimeList[i]._scrore.z == playerData._scrore.z)
            {
                sameHitList.Add(sameTimeList[i]);
                lastnesstle = false;
            }
            else if (sameTimeList[i]._scrore.z > playerData._scrore.z)
            {
                higherHitList.Add(sameTimeList[i]);
            }
            else if (sameTimeList[i]._scrore.z < playerData._scrore.z)
            {
                lowerHitList.Add(sameTimeList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherHitList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for accucracy
        for (int i = 0; i < sameHitList.Count; i++)
        {
            if (sameHitList[i]._scrore.w == playerData._scrore.w)
            {
                sameAccuracyList.Add(sameHitList[i]);
            }
            else if (sameHitList[i]._scrore.w > playerData._scrore.w)
            {
                higherAccuracyList.Add(sameHitList[i]);
            }
            else if (sameHitList[i]._scrore.w < playerData._scrore.w)
            {
                lowerAccuracyList.Add(sameHitList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherAccuracyList.Add(playerData);
            playerHasBeenAdded = true;
        }


        // put it all in one list
        for (int i = 0; i < ListArray.Length; i++)
        {
            foreach (PlayerDataStruct score in ListArray[i])
            {
                sortedList.Add(score);
            }

        }


        DateTime endTime = DateTime.Now;
        TimeSpan timePast = endTime - startTime;
        Debug.Log(String.Format("Time Spent: {0} Milliseconds", timePast.TotalMilliseconds));

        return sortedList;
    }
    private List<Vector4> SortScore(Vector4 playerData, List<Vector4> allScores)
    {
        Debug.Log("===============");
        Debug.Log("Sorting Grade");

        List<Vector4> sameGradeList = new List<Vector4>();
        List<Vector4> higherGradeList = new List<Vector4>();
        List<Vector4> lowerGradeList = new List<Vector4>();


        List<Vector4> sameTimeList = new List<Vector4>();
        List<Vector4> higherTimeList = new List<Vector4>();
        List<Vector4> lowerTimeList = new List<Vector4>();


        List<Vector4> sameHitList = new List<Vector4>();
        List<Vector4> higherHitList = new List<Vector4>();
        List<Vector4> lowerHitList = new List<Vector4>();

        List<Vector4> sameAccuracyList = new List<Vector4>();
        List<Vector4> higherAccuracyList = new List<Vector4>();
        List<Vector4> lowerAccuracyList = new List<Vector4>();

        List<Vector4> sortedList = new List<Vector4>();

        List<Vector4>[] ListArray =
        {
            higherGradeList,
            higherTimeList,
            higherHitList,
            higherAccuracyList,
            sameAccuracyList,
            lowerAccuracyList,
            lowerHitList,
            lowerTimeList,
            lowerGradeList
        };

        DateTime startTime = DateTime.Now;
        bool lastnesstle = true;
        bool playerHasBeenAdded = false;


        // check for same Grade 
        for (int i = 0; i < allScores.Count; i++)
        {
            if (allScores[i].x == playerData.x)
            {
                sameGradeList.Add(allScores[i]);
                lastnesstle = false;
            }
            else if (allScores[i].x < playerData.x)
            {
                higherGradeList.Add(allScores[i]);
            }
            else if (allScores[i].x > playerData.x)
            {
                lowerGradeList.Add(allScores[i]);
            }

        }

        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherGradeList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for time 
        for (int i = 0; i < sameGradeList.Count; i++)
        {
            if (sameGradeList[i].y == playerData.y)
            {
                sameTimeList.Add(sameGradeList[i]);
                lastnesstle = false;
            }
            else if (sameGradeList[i].y < playerData.y)
            {
                higherTimeList.Add(sameGradeList[i]);
            }
            else if (sameGradeList[i].y > playerData.y)
            {
                lowerTimeList.Add(sameGradeList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherTimeList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for hits
        for (int i = 0; i < sameTimeList.Count; i++)
        {
            if (sameTimeList[i].z == playerData.z)
            {
                sameHitList.Add(sameTimeList[i]);
                lastnesstle = false;
            }
            else if (sameTimeList[i].z > playerData.z)
            {
                higherHitList.Add(sameTimeList[i]);
            }
            else if (sameTimeList[i].z < playerData.z)
            {
                lowerHitList.Add(sameTimeList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherHitList.Add(playerData);
            playerHasBeenAdded = true;
        }

        lastnesstle = true;
        // check for accucracy
        for (int i = 0; i < sameHitList.Count; i++)
        {
            if (sameHitList[i].w == playerData.w)
            {
                sameAccuracyList.Add(sameHitList[i]);
            }
            else if (sameHitList[i].w > playerData.w)
            {
                higherAccuracyList.Add(sameHitList[i]);
            }
            else if (sameHitList[i].w < playerData.w)
            {
                lowerAccuracyList.Add(sameHitList[i]);
            }

        }
        if (lastnesstle && playerHasBeenAdded == false)
        {
            higherAccuracyList.Add(playerData);
            playerHasBeenAdded = true;
        }


        // put it all in one list
        for (int i = 0; i < ListArray.Length; i++)
        {
            foreach (Vector4 score in ListArray[i])
            {
                sortedList.Add(score);
            }

        }


        DateTime endTime = DateTime.Now;
        TimeSpan timePast = endTime - startTime;
        Debug.Log(String.Format("Time Spent: {0} Milliseconds", timePast.TotalMilliseconds));

        return sortedList;
    }

    private List<PlayerDataStruct> FillInTestOldHighScores()
    {
        List<PlayerDataStruct> list = new List<PlayerDataStruct>();
        // 1 50 2 90
        PlayerDataStruct score1 = new PlayerDataStruct { rank = 1, _name = "score1", _scrore = new Vector4(0, 40, 4, 95) };
        PlayerDataStruct score2 = new PlayerDataStruct { rank = 1, _name = "score2", _scrore = new Vector4(1, 30, 4, 90) };
        PlayerDataStruct score3 = new PlayerDataStruct { rank = 1, _name = "score3", _scrore = new Vector4(1, 40, 3, 95) };
        PlayerDataStruct score4 = new PlayerDataStruct { rank = 1, _name = "score4", _scrore = new Vector4(1, 40, 3, 90) };
        PlayerDataStruct score5 = new PlayerDataStruct { rank = 1, _name = "score5", _scrore = new Vector4(1, 40, 2, 95) };
        PlayerDataStruct score6 = new PlayerDataStruct { rank = 1, _name = "score6", _scrore = new Vector4(1, 50, 4, 90) };


        list.Add(score1);
        list.Add(score2);
        list.Add(score3);
        list.Add(score4);
        list.Add(score5);
        list.Add(score6);

        return list;
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
        string typedName = keyBoard.TypedName;
        Debug.Log("name player :" + typedName);

        currentPlayerData._name = typedName;

        for(int i = newHighScore.Count -1; i >= 0; i--)
        {
            if (newHighScore[i]._name != defaultPlayerName) continue;
            newHighScore[i] = currentPlayerData;
        }

        //List<PlayerDataStruct> newHighScore = LinkScoreWithPlayerData(oldHighScore, sortedScores, currentPlayerData);
        //oldHighScore = newHighScore;
        //OnHighScoreDataIsDone?.Invoke(newHighScore);
        OnRestartLevel?.Invoke();
        string path = GetPath();
        SaveList(path, newHighScore);
        //oldHighScore = newHighScore;
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
    private PlayerDataStruct GetPlayerData()
    {
        Vector4 newData = getPlayerData.GetData();
        Debug.Log(newData);
        PlayerDataStruct playerData = new PlayerDataStruct();
        //playerData._name = "test player";
        //playerData._time = levelManager.PlayerGameStopWatch.currentTime;
        playerData._scrore = newData;

        return playerData;
    }

    private void SaveList(string path, List<PlayerDataStruct> newHighScores)
    {
        LevelDataStruct newlevelDataStruct = new LevelDataStruct();
        newlevelDataStruct._level = 1;
        newlevelDataStruct._highScores = newHighScores;



        StreamWriter streamWriter = new StreamWriter(path, false);
        streamWriter.WriteLine(JsonUtility.ToJson(newlevelDataStruct, true));
        streamWriter.Close();
        streamWriter.Dispose();
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
    private List<float> SortNewScoreNew(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> grade = new List<float>();


        grade.Add(currentPlayerData._time);


        foreach (PlayerDataStruct playerDataStruct in oldHighscoreData)
        {
            grade.Add(playerDataStruct._time);
        }

        grade.Sort();

        while (grade.Count > maxHighScores)
        {
            grade.Remove(grade[grade.Count - 1]);
        }

        return grade;
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
    public int rank;
    public Vector4 _scrore;
}

[Serializable]
public struct LevelDataStruct
{
    public int _level;
    public List<PlayerDataStruct> _highScores;
}

