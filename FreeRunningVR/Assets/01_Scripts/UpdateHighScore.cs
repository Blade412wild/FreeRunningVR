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
    [SerializeField] private List<TextMeshProUGUI> gradeList;

    [SerializeField] private List<TextMeshProUGUI> timeList;
    [SerializeField] private List<TextMeshProUGUI> hitList;
    [SerializeField] private List<TextMeshProUGUI> accuracyList;

    private string[] grades = { "S", "A+", "A", "B+", "B", "C+", "C", "F" };

    private void Start()
    {
        highScoreManager.OnHighScoreDataIsDone += UpdateHighScoreUI;
    }


    private void UpdateHighScoreUI(List<PlayerDataStruct> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            namesList[i].text = list[i]._name;
            gradeList[i].text = grades[(int)list[i]._scrore.x];
            timeList[i].text = String.Format("{0:F2}", list[i]._scrore.y) + " sec";
            hitList[i].text = list[i]._scrore.z.ToString() + "/" + highscoreRequirements.Requirements[0].z;
            accuracyList[i].text = String.Format("{0:F2}", list[i]._scrore.w) + " %";
        }
    }
}

