using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUIUpdate : MonoBehaviour
{

    [SerializeField] private HighscoreRequirements highscoreRequirements;

    [SerializeField] private TextMeshProUGUI gradeList;
    [SerializeField] private TextMeshProUGUI timeList;
    [SerializeField] private TextMeshProUGUI hitList;
    [SerializeField] private TextMeshProUGUI accuracyList;

    private string[] grades = { "S", "A+", "A", "B+", "B", "C+", "C", "F" };

    public void UpdateHighScoreUI(Vector4 score)
    {
        //namesList.text = score._name;
        gradeList.text = grades[(int)score.x];
        timeList.text = String.Format("{0:F2}", score.y) + " sec";
        hitList.text = score.z.ToString() + "/" + highscoreRequirements.Requirements[0].z;
        accuracyList.text = String.Format("{0:F2}", score.w) + " %";

    }
}
