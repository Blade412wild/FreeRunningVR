using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StopWatch PlayerGameStopWatch { get; private set; }

    private void Start()
    {
        PlayerGameStopWatch = new StopWatch();
    }

    private void Update()
    {
        PlayerGameStopWatch.OnUpdate();
        Debug.Log(PlayerGameStopWatch.currentTime);

    }



}
