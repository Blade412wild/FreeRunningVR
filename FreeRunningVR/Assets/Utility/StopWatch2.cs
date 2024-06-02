using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch2 : MonoBehaviour
{
    private float startTime = 0;
    public float currentTime;
    private bool mayRun = true;

    public StopWatch2()
    {

    }


    // Update is called once per frame
    public void OnUpdate()
    {
        if (!mayRun) return;
        currentTime += Time.deltaTime;
    }
    public void PauseStopWatch()
    {
        mayRun = false;
    }

    public void ContinueStopWatch()
    {
        mayRun = true;
    }

    public void ResetTimer()
    {
        currentTime = startTime;
    }
}
