using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public event Action<Collider> OnColliderEnter;
    public event Action<Collider> OnColliderExit;

    private Collider collider;

    public void GetCollider()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnColliderEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnColliderExit?.Invoke(other);
    }
}
