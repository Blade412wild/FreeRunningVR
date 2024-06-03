using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    public event Action<JumpObject, Collider> OnPlayerEnterTrigger;

    private Collider collider;

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnterTrigger?.Invoke(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void GetCollider()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void TurnToTrigger()
    {
        collider.isTrigger = true;
    }

    public void TurnToCollider()
    {
        collider.isTrigger = false;
    }
}
