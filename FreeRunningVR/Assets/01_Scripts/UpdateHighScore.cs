using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateHighScore : MonoBehaviour
{
    [SerializeField] private HighScoreManager highScoreManager;

    [SerializeField] private List<TextMeshProUGUI> namesList;
    [SerializeField] private List<TextMeshProUGUI> scoreList;

    private void Start()
    {
        highScoreManager.OnHighScoreDataIsDone += UpdateHighScoreUI;
    }

    private void UpdateHighScoreUI(List<PlayerDataStruct> list)
    {
        for(int i  = 0; i < list.Count; i++)
        {
            namesList[i].text = list[i]._name;
            //scoreList[i].text = list[i]._time.ToString();
            scoreList[i].text = String.Format("{0:F2}", list[i]._time);

        }

    }
}
