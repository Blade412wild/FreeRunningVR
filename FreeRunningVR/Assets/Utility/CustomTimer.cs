using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



[CreateAssetMenu(fileName = "CustomTimer", menuName = "ScriptableObjects/CustomTimer", order = 2)]
public class CustomTimer : ScriptableObject
{
    [Header("TimerSettings")]
    public float timerDuration;
    public bool repeat;
    public int repeatAmount;
    public bool giveTimeBack;

    public Timer timerInstance {get; set;}
    public void CreateTimer()
    {
        TimerManager.Instance.AddTimerToList(timerInstance = new Timer(this));
    }

    public int ShowTime()
    {
        return timerInstance.ShowTime();
    }


}
