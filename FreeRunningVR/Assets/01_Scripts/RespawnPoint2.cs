using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnPoint2 : MonoBehaviour
{
    public event Action<RespawnPoint> OnTriggerEnterEvent;

    [SerializeField] private RespawnPoint spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(spawnPoint);
    }
}
