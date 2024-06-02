using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    // events 
    public event Action OnTriggerTimerIsDone;

    // scripts 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MiniGameTrigger trigger;
    [SerializeField] private Animator animator;
    [SerializeField] private PistolManager pistolManager;
    private GameActivator gameActivor;
    private TargetManager2 targetManager;

    // objects
    [SerializeField] private List<Target> targets;
    [SerializeField] private GameObject enterTriggerObject;
    [SerializeField] private GameObject exitTriggerObject;
    [SerializeField] private Transform targertSpawnLocation;
    [SerializeField] private List<TargetLevel> targetLevels;


    private Collider playerCollider;
    private Timer1 timer;
    private bool mayTimerUpdate = false;
    private bool isEventSet = false;


    // transisitions 
    private bool playerisInCollider = false;
    private bool isTimerDone = false;
    private bool areAllTargetHit = false;

    private StateMachine2 stateMachine;

    private void Start()
    {
        SetupTargetManager();
        SetupStateMachine();
        gameManager.OnSpawnPlayerDone += BeginMiniGameInilisation;
    }

    private void Update()
    {
        if (mayTimerUpdate)
        {
            timer.OnUpdate();
        }
    }
    private void FixedUpdate()
    {
        stateMachine.OnFixedUpdate();
    }

    private void BeginMiniGameInilisation()
    {
        playerCollider = GetPlayerCollider();
        gameActivor = new GameActivator(trigger, playerCollider, enterTriggerObject, exitTriggerObject);
        gameActivor.OnPlayerExitCollider += PlayerIsOutOfCollider;
        gameActivor.OnPlayerEntersCollider += BeginGame;
    }

    private void BeginGame()
    {
        animator.SetTrigger("open");
        playerisInCollider = true;
    }

    private void PlayerIsOutOfCollider(Timer1 triggerTimer)
    {
        TurnTimerOn(triggerTimer);
        playerisInCollider = false;
    }


    private void TurnTimerOn(Timer1 triggerTimer)
    {
        timer = triggerTimer;
        isTimerDone = false;
        timer.ResetTimer();
        mayTimerUpdate = true;
        SwitchToIdle();

        if(isEventSet == false)
        {
            timer.OnTimerIsDone += TimerIsDone;
        }
    }

    private void TimerIsDone()
    {
        isTimerDone = true; 
    }

    private void SwitchToIdle()
    {
        //animator.SetTrigger("close");
        //TriggerTimerDone?.Invoke();
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
    public bool CheckifPlayerIsInTrigger()
    {
        return playerisInCollider;
    }

    private bool CheckifTimerIsDone()
    {
        return isTimerDone;
    }

    public bool CheckIfAllTargetsAreHit()
    {
        return areAllTargetHit;
    }

    private void SetupTargetManager()
    {
        targetManager = new TargetManager2(pistolManager,targertSpawnLocation, targetLevels);
        targetManager.OnAllTargetsHit += SetTargetSToTrue;
        targetManager.OnBuildLevel += SetTargetToFalse;
    }

    private void SetTargetSToTrue()
    {
        Debug.Log("Event");
        areAllTargetHit = true;
    }

    private void SetTargetToFalse()
    {
        areAllTargetHit = false;
    }
    public void SetupStateMachine()
    {
        //creating States
        IState idle = new Idle(this, targetManager);
        IState playing = new Playing(this);
        IState ending = new Ending(this);

        stateMachine = new StateMachine2(idle, playing, ending);

        stateMachine.AddTransition(new Transition(idle, playing, CheckifPlayerIsInTrigger));
        //stateMachine.AddTransition(new Transition(playing, idle, CheckifTimerIsDone));
        stateMachine.AddTransition(new Transition(playing, ending, CheckIfAllTargetsAreHit));
        stateMachine.AddTransition(new Transition(ending, idle, () => { return true; }));

        stateMachine.SwitchState(idle);
    }

}
