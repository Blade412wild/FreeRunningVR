using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    private Timer1 timer;

    private void Start()
    {
        timer = new Timer1(duration);
        timer.OnTimerIsDone += DestroyBullet;
    }

    void Update()
    {
        timer.OnUpdate();
    }

    private void DestroyBullet()
    {
       Destroy(gameObject);
    }
}
