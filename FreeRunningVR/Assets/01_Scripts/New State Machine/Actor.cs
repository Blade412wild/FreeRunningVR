using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StateMachine2;

public class Actor : MonoBehaviour
{
    public float Health = 100;
    private StateMachine2 stateMachine;
    private void Start()
    {
        SetupStateMachine();
    }
    private void SetupStateMachine()
    {
        // create states
        IState locomotionState = new LocomotionState(this);
        IState anotherState = new AnotherState(this);

        // create state machine 
        stateMachine = new StateMachine2(locomotionState, anotherState);

        // create transisitions 
        stateMachine.AddTransition(new Transition(locomotionState,anotherState, IsDead)); //We can create the condition as a delegate
        //stateMachine.AddTransition(new Transition(null, locomotionState, () => Health > 0)); //Or as a lambda expression. By omitting the FromState, the transition works for any state


        stateMachine.SwitchState(locomotionState);
    }
    public void Attack()
    {
    }
    private void FixedUpdate()
    {
        stateMachine.OnUpdate();
    }
    public bool IsDead()
    {
        return Health <= 0;
    }
}
