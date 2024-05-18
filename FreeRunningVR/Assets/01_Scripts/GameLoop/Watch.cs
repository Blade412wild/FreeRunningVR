using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Watch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI watchUI;
    private LevelManager levelManager;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        watchUI.text = "00:00";
    }

    // Update is called once per frame
    void Update()
    {
        float time = levelManager.PlayerGameStopWatch.currentTime;
        int seconds = ((int)time % 60);
        int minutes = ((int)time / 60);
        string UITime = string.Format("{0:00}:{1:00}", minutes, seconds);
        watchUI.text = UITime.ToString();
    }
}
