using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffects : MonoBehaviour
{
    [SerializeField] private CustomTimer timer;

    private void Start()
    {
        timer.CreateTimer();
        timer.timerInstance.OnTimerIsDonePublic += DestroyObject;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
