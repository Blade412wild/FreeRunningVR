using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
    public StopWatch playTimeTimer { get; private set; }
    public ScoreManager()
    {
        playTimeTimer = new StopWatch();
    }

    public void CalculateScore()
    {

    }
}
