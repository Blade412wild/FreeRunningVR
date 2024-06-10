using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBlock : MonoBehaviour
{
    public event Action OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke();
    }
}
