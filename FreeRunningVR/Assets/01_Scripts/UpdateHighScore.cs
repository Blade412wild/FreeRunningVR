using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateHighScore : MonoBehaviour
{
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private HighscoreRequirements highscoreRequirements;
    [SerializeField] private List<TextMeshProUGUI> namesList;
    [SerializeField] private List<TextMeshProUGUI> timeList;
    [SerializeField] private List<TextMeshProUGUI> hitList;
    [SerializeField] private List<TextMeshProUGUI> accuracyList;


    private void Start()
    {
        highScoreManager.OnHighScoreDataIsDone += UpdateHighScoreUI;
        LevelDataStruct highscoreFile = CreateNewHighScore();
        highscoreFile._highScores = FillInTestOldHighScores();
        UpdateHighScoreUI(highscoreFile._highScores);
    }

    private void UpdateHighScoreUI(List<PlayerDataStruct> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            namesList[i].text = list[i]._name;
            timeList[i].text = String.Format("{0:F2}", list[i]._scrore.y) + " sec";
            hitList[i].text = list[i]._scrore.z.ToString() + "/" + highscoreRequirements.Requirements[0].z;
            accuracyList[i].text = String.Format("{0:F2}", list[i]._scrore.w);
        }
    }
    private List<PlayerDataStruct> FillInTestOldHighScores()
    {
        List<PlayerDataStruct> list = new List<PlayerDataStruct>();
        // 1 50 2 90
        PlayerDataStruct score1 = new PlayerDataStruct { rank = 1, _name = "score1", _scrore = new Vector4(0, 40, 4, 95) };
        PlayerDataStruct score2 = new PlayerDataStruct { rank = 1, _name = "score2", _scrore = new Vector4(1, 30, 4, 90) };
        PlayerDataStruct score3 = new PlayerDataStruct { rank = 1, _name = "score3cccc", _scrore = new Vector4(1, 40, 3, 95) };
        PlayerDataStruct score4 = new PlayerDataStruct { rank = 1, _name = "zzzzzzzzzz", _scrore = new Vector4(1, 40, 3, 90) };
        PlayerDataStruct score5 = new PlayerDataStruct { rank = 1, _name = "mmmmmmmmmm", _scrore = new Vector4(1, 40, 2, 95) };

        list.Add(score1);
        list.Add(score2);
        list.Add(score3);
        list.Add(score4);
        list.Add(score5);


        return list;
    }


    private LevelDataStruct CreateNewHighScore()
    {
        LevelDataStruct newlevelDataStruct = new LevelDataStruct();
        newlevelDataStruct._level = 1;
        newlevelDataStruct._highScores = new List<PlayerDataStruct>();
        return newlevelDataStruct;
    }
}

