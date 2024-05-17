using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private float startTime = 0;
    public float currentTime;
    private float endTime;
    private bool repeat = false;
    private int repeatAmount = 0;
    private int currentAmount = 1;
    private bool mayRun = true;

    // Update is called once per frame
    public void OnUpdate()
    {
        if (!mayRun) return;
        currentTime += Time.deltaTime;


        //Debug.Log(currentTime);
        //RunStopWatch();
    }

    private void RunStopWatch()
    {
        if (currentTime >= endTime)
        {
            //Debug.Log(" Timer is finished, [" + endTime + "] have past");
            if (repeat == true && currentAmount < repeatAmount)
            {
                Debug.Log(" repeat amount = " + currentAmount);
                Debug.Log("repeat Timer");
                var t = Time.time;
                Debug.Log(t);
                currentTime = 0;
                currentAmount++;
            }
            else
            {
                //OnRemoveTimer?.Invoke(this);
            }
        }
    }

    public void ResetTimer()
    {
        currentTime = startTime;
    }
}
