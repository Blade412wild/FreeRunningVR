using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    // scripts 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MiniGameTrigger trigger;
    [SerializeField] private Animator animator;
    private GameActivator gameActivor;

    // objects
    [SerializeField] private List<Target> targets;
    [SerializeField] private GameObject enterTriggerObject;
    [SerializeField] private GameObject exitTriggerObject;

    private Collider playerCollider;
    private Timer1 timer;

    private void Start()
    {
        gameManager.OnSpawnPlayerDone += BeginMiniGameInilisation;
        SetupStateMachine();
    }

    private void BeginMiniGameInilisation()
    {
        playerCollider = GetPlayerCollider();
        gameActivor = new GameActivator(trigger, playerCollider, enterTriggerObject, exitTriggerObject);
        gameActivor.OnPlayerExitCollider += TurnTimerOn;
        gameActivor.OnPlayerEntersCollider += BeginGame;
    }

    private void BeginGame()
    {
        animator.SetTrigger("open");
    }

    private void TurnTimerOn(Timer1 triggerTimer)
    {
        timer = triggerTimer;
    }

    private Collider GetPlayerCollider()
    {
        foreach (Collider collider in gameManager.playerData.Colliders)
        {
            if (collider.gameObject.GetComponent<CapsuleCollider>() != null)
            {
                return collider;
            }
        }
        return null;
    }

    private void Update()
    {
        if (timer != null)
        {
            timer.OnUpdate();
        }
    }

    private void SetupStateMachine()
    {
        //creating States
        IState idle = new Idle(this);
        IState playing = new Playing(this);
        IState Ending = new Ending(this);







        StateMachine2 stateMachine = new StateMachine2();
    }

}
