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
    private PlayerDataStruct currentPlayerData;
    private GetPlayerData getPlayerData;
    private List<float> sortedScores;


    private void Start()
    {
        levelManager.OnEndLevel += PlayerIsFinished;
        Keyboard.OnInsertedName += FinalizeHighScore;

        getPlayerData = new GetPlayerData(highscoreRequirements, levelManager, targetManager);
        //LoadHighScoreOnStart();

        PlayerIsFinished();

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
        currentPlayerData = GetPlayerData();
        LevelDataStruct highScoreFile = CreateNewHighScore();
        highScoreFile._highScores = FillInTestOldHighScores();
        oldHighScore = highScoreFile._highScores;


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

        List<Vector4> sortedList = SortScore(currentPlayerData._scrore, allScores);

        //foreach(PlayerDataStruct playerDataStruct in allScoresAndPlayer)
        //{
        //    for(int i = 0; i < sortedList.Count; i++)
        //    {
        //        if (sortedList[i] != playerDataStruct._scrore) continue;
        //        Debug.Log(playerDataStruct._name + " : " + playerDataStruct._scrore);
        //    }
        //}


        Debug.Log("===== Sorted List =====");
        foreach (Vector4 score in sortedList)
        {
            for (int i = 0; i < allScoresAndPlayer.Count; i++)
            {
                if (score != allScoresAndPlayer[i]._scrore) continue;
                Debug.Log(allScoresAndPlayer[i]._name + " : " + allScoresAndPlayer[i]._scrore);
            }
        }


        //// get scores with the same grade

        //List<Vector4> scoreswithSameGrade = new List<Vector4>();
        //foreach (Vector4 score in allScores)
        //{
        //    if (score.x != currentPlayerData._scrore.x) continue;
        //    scoreswithSameGrade.Add(score);
        //}


        ////check scores and get names
        //Debug.Log("===== Same Scores =====");
        //foreach (PlayerDataStruct playerDataStruct in oldHighScore)
        //{
        //    for (int i = 0; i < scoreswithSameGrade.Count; i++)
        //    {
        //        if (playerDataStruct._scrore.x != scoreswithSameGrade[i].x) continue;
        //        Debug.Log(playerDataStruct._name + " : " + playerDataStruct._scrore);
        //    }
        //}
        //scoreswithSameGrade.Add(currentPlayerData._scrore);
        //Debug.Log(currentPlayerData._name + " : " + currentPlayerData._scrore);







        ///////







        //string path = GetPath();
        //LevelDataStruct? highScoreFile = LoadList(path);

        //if (highScoreFile == null)
        //{
        //    //highScoreFile.Value._highScores.Add(currentPlayerData);
        //    highScoreFile = CreateNewHighScore();

        //    oldHighScore = highScoreFile.Value._highScores;
        //    //sortedScores = new List<float> { currentPlayerData._time };
        //    //MayFillInName = true;

        //}
        //else
        //{
        //    oldHighScore = highScoreFile.Value._highScores;
        //    List<float> grades = SortNewHighScoreOnGrade(oldHighScore, currentPlayerData);
        //    mayFillInName = isPlayerOnScoreboard(grades, currentPlayerData); // ga eruit als de speler niet on the board mag

        //    List<Vector4> allScores = new List<Vector4>();

        //    foreach(PlayerDataStruct playerDataStruct in oldHighScore)
        //    {
        //        allScores.Add(playerDataStruct._scrore);
        //    }

        //    List<float> scoreswithSameGrade = new List<float>();





        //    //sortedScores = SortNewScoreNew(highScoreFile.Value._highScores, currentPlayerData);
        //    //MayFillInName = CheckIfPlayerMayFillInName(sortedScores, currentPlayerData);
        //}



        if (mayFillInName)
        {
            OnInsertName?.Invoke();
        }
        else
        {
            OnRestartLevel?.Invoke();
        }
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
                lastnesstle = false;
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

    //private void CalculateScore(Vector4 playerData, List<Vector4> sameGrades)
    //{
    //    Debug.Log("===============");
    //    Debug.Log("Sorting Grade");

    //    List<Vector4> sortedList = new List<Vector4>();
    //    List<Vector4> unsortedList = sameGrades;
    //    List<Vector4> checkAllUnsorted = new List<Vector4>();

    //    DateTime startTime = DateTime.Now;
    //    Vector4 currentHighistScore = sameGrades[0];

    //    while (unsortedList.Count != 0)
    //    {

    //        // check for same Grade 
    //        for (int i = 0; i < unsortedList.Count; i++)
    //        {
    //            // check for time 
    //            for (int j = 0; i < unsortedList.Count; j++)
    //            {
    //                // check for hits
    //                for (int k = 0; k < unsortedList.Count; k++)
    //                {
    //                    // check for accucracy
    //                    for (int  l = 0; l < unsortedList.Count; l++)
    //                    {

    //                    }
    //                }
    //            }


    //            Debug.Log("Newest highest score : " + unsortedList[i]);
    //            sortedList.Add(unsortedList[i]);
    //            unsortedList.Remove(unsortedList[i]);

    //            break;
    //        }
    //    }

    //    DateTime endTime = DateTime.Now;
    //    TimeSpan timePast = endTime - startTime;
    //    Debug.Log(String.Format("Time Spent: {0} Milliseconds", timePast.TotalMilliseconds));
    //}


    //private void CalculateScore(Vector4 playerData, List<Vector4> sameGrades)
    //{
    //    Debug.Log("===============");
    //    Debug.Log("Sorting Grade");

    //    List<Vector4> sortedList = new List<Vector4>();
    //    List<Vector4> unsortedList = sameGrades;
    //    List<Vector4> checkAllUnsorted = new List<Vector4>();

    //    DateTime startTime = DateTime.Now;
    //    Vector4 currentHighistScore = sameGrades[0];

    //    while (unsortedList.Count != 0)
    //    {
    //        for (int i = 0; i < unsortedList.Count; i++)
    //        {
    //            bool isReallyTheHighest = false;
    //            for (int j = 0; i < checkAllUnsorted.Count; j++)
    //            {
    //                if (CheckIfTheRequirementsAreMet(unsortedList[i].y, currentHighistScore.y, true) == false) continue;
    //                Debug.Log("time is good");
    //                isReallyTheHighest = false;
    //                if (CheckIfTheRequirementsAreMet(unsortedList[i].z, currentHighistScore.z, false) == false) continue;
    //                Debug.Log("hits are good");
    //                isReallyTheHighest = false;
    //                if (CheckIfTheRequirementsAreMet(unsortedList[i].w, currentHighistScore.w, false) == false) continue;
    //                Debug.Log("accuracy is good");
    //                isReallyTheHighest = true;
    //            }


    //            Debug.Log("Newest highest score : " + unsortedList[i]);
    //            sortedList.Add(unsortedList[i]);
    //            unsortedList.Remove(unsortedList[i]);

    //            break;
    //        }
    //    }

    //    DateTime endTime = DateTime.Now;
    //    TimeSpan timePast = endTime - startTime;
    //    Debug.Log(String.Format("Time Spent: {0} Milliseconds", timePast.TotalMilliseconds));
    //}

    private bool CheckIfTheRequirementsAreMet(float playerscore, float requiredScore, bool reverse)
    {
        // reverse moet true zijn wanneer de playerdata kleiner moet zijn dan je requirements
        if (reverse == true)
        {
            if (playerscore >= requiredScore) return false;
            return true;
        }
        else
        {
            if (playerscore < requiredScore) return false;
            return true;

        }
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

    private void CalulcateNewHighScore(List<PlayerDataStruct> oldHighscoreData)
    {
        List<Vector4> scores = new List<Vector4>();


        for (int i = 0; i < maxHighScores; i++)
        {
        }
    }


    private bool isPlayerOnScoreboard(List<float> grades, PlayerDataStruct currentPlayerData)
    {
        foreach (float grade in grades)
        {
            if (grade == currentPlayerData._scrore.x) return true;
        }

        return false;
    }

    private bool CheckIfScoreIsSameGrade(List<float> grades)
    {
        float refrences = grades[0];
        bool isSameGrade = false;
        foreach (float grade in grades)
        {
            if (grade != refrences) break;
            isSameGrade = true;

        }
        return isSameGrade;
    }

    private List<float> SortNewHighScoreOnGrade(List<PlayerDataStruct> oldHighscoreData, PlayerDataStruct currentPlayerData)
    {
        // seperate the score;
        List<float> grade = new List<float>();

        grade.Add(currentPlayerData._scrore.x);


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

    private bool CheckIfPlayerMayFillInName(List<float> sortedScores, PlayerDataStruct currentPlayerData)
    {
        foreach (float score in sortedScores)
        {
            if (score == currentPlayerData._time)
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
        OnRestartLevel?.Invoke();
        string path = GetPath();
        SaveList(path, newHighScore);
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
        playerData._name = "test player";
        //playerData._time = levelManager.PlayerGameStopWatch.currentTime;
        playerData._scrore = newData;

        return playerData;
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

