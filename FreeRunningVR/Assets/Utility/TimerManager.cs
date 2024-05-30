using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;
    public List<Timer> timersList = new List<Timer>();
    public List<Timer> timersTobeRemoved = new List<Timer>();


    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newTimeManger = new GameObject();
                TimerManager timeManager = newTimeManger.AddComponent<TimerManager>();
                newTimeManger.transform.name = "TimeManager";
                instance = timeManager;
            }

            return instance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Timer timer in timersList)
        {
            timer.OnUpdate();
        }

        foreach (Timer timer in timersTobeRemoved)
        {
            timersList.Remove(timer);
        }

        timersTobeRemoved.Clear();
    }

    
    public void RemoveTimerFromList(Timer _timer)
    {
        //Debug.Log(" removed timer from list");
        //timers.Remove(_timer);
        timersTobeRemoved.Add(_timer);
    }

    public void AddTimerToList(Timer _timer)
    {
        _timer.OnRemoveTimer += RemoveTimerFromList;
        timersList.Add(_timer);
    }
}
