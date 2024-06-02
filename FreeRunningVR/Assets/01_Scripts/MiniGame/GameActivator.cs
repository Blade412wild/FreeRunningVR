using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameActivator
{
    public event Action OnPlayerEntersCollider;
    public event Action<Timer1> OnPlayerExitCollider;

    private MiniGameTrigger miniGameTrigger;
    private Collider playerCollider;
    private GameObject triggerBodyEnterObject;
    private GameObject triggerBodyExitObject;

    private Timer1 triggerDisableTimer;
    private float triggerDisableTime = 5.0f;

    public GameActivator(MiniGameTrigger miniGameTrigger, Collider playerCollider, GameObject enterObject, GameObject exitObject)
    {
        this.miniGameTrigger = miniGameTrigger;
        this.playerCollider = playerCollider;
        triggerBodyEnterObject = enterObject;
        triggerBodyExitObject = exitObject;
        triggerDisableTimer = new Timer1(triggerDisableTime);

        SetEventsListeners();
    }

    private void SetEventsListeners()
    {
        miniGameTrigger.OnColliderEnter += HandleEnter;
        miniGameTrigger.OnColliderExit += HandleExit;
        triggerDisableTimer.OnTimerIsDone += ResetTriggerObjects;
    }

    public void RemoveEventsListeners()
    {
        miniGameTrigger.OnColliderEnter -= HandleEnter;
        miniGameTrigger.OnColliderExit -= HandleExit;
    }

    private bool IsColliderPlayer(Collider otherCollider)
    {
        if (otherCollider == playerCollider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleEnter(Collider otherCollider)
    {
        if (IsColliderPlayer(otherCollider) == false) return;
        ObjectsActive(false, false);
        OnPlayerEntersCollider?.Invoke();
    }

    private void HandleExit(Collider otherCollider)
    {
        if (IsColliderPlayer(otherCollider) == false) return;

        ObjectsActive(false, true);
        OnPlayerExitCollider?.Invoke(triggerDisableTimer);
    }

    private void ObjectsActive(bool enterObject, bool exitObject)
    {
        triggerBodyEnterObject.SetActive(enterObject);
        triggerBodyExitObject.SetActive(exitObject);
    }

    private void ResetTriggerObjects()
    {
        ObjectsActive(true, false);
    }
}
