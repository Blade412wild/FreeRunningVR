using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Timer
{
    public static event Action OnTimerIsDone;
    public event Action OnTimerIsDonePublic;
    public event Action<Timer> OnRemoveTimer;

    // timer 
    private float startTime;
    private float currentTime = 0;
    private int endTime = 0;
    private bool repeat = false;
    private int repeatAmount = 0;
    private int currentAmount = 1;
    private bool giveTImeBack = false;

    public Timer(CustomTimer _customTimer)
    {
        startTime = _customTimer.timerDuration;
        currentTime = startTime;
        repeat = _customTimer.repeat;
        repeatAmount = _customTimer.repeatAmount;
        giveTImeBack = _customTimer.giveTimeBack;
    }

    public Timer(int _seconds, bool _repeat)
    {
        startTime = _seconds;
        currentTime = startTime;
        repeat = _repeat;
    }

    public Timer(int _seconds, bool _repeat, int _amount)
    {
        startTime = _seconds;
        currentTime = startTime;
        repeat = _repeat;
        repeatAmount = _amount;
    }



    // Update is called once per frame
    public void OnUpdate()
    {
        currentTime -= Time.deltaTime;
        RunTimer();
    }

    private void RunTimer()
    {
        if (currentTime <= endTime)
        {
            //Debug.Log(" Timer is finished, [" + endTime + "] have past");
            if (repeat == true && currentAmount < repeatAmount)
            {
               // Debug.Log(" repeat amount = " + currentAmount);
                //Debug.Log("repeat Timer");
                currentTime = startTime;
                currentAmount++;
                OnTimerIsDone?.Invoke();
                OnTimerIsDonePublic?.Invoke();
            }
            else
            {
                OnTimerIsDone?.Invoke();
                OnRemoveTimer?.Invoke(this);
                OnTimerIsDonePublic?.Invoke();

            }
        }
    }

    public int ShowTime()
    {
        var time = (int)currentTime;
        return time;
    }
}
