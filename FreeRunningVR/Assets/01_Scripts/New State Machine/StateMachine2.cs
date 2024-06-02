using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine2 : MonoBehaviour
{
    private List<Transition> allTransitions = new List<Transition>();
    private List<Transition> activeTransitions = new List<Transition>();
    private IState currentState;
    private Dictionary<Type, IState> stateCollection = new Dictionary<Type,
    IState>();
    public StateMachine2(params IState[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            stateCollection.Add(states[i].GetType(), states[i]);
        }
    }
    public void OnUpdate()
    {
        foreach (Transition transition in activeTransitions)
        {
            if (transition.Evalutate())
            {
                SwitchState(transition.toState);
                return;
            }
        }
        currentState?.OnUpdate();
    }


    public void SwitchState(System.Type newStateType)
    {
        if (stateCollection.ContainsKey(newStateType))
        {
            SwitchState(stateCollection[newStateType]);
        }
        else
        {
            //Debug.LogError($"State {newStateType.ToString()} not found in
            //stateCollection");
        }
    }
    public void SwitchState(IState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        activeTransitions = allTransitions.FindAll(x => x.fromState ==
        currentState || x.fromState == null);
        currentState?.OnEnter();
    }
    public void AddState(IState state)
    {
        stateCollection.Add(state.GetType(), state);
    }
    public void AddTransition(Transition transition)
    {
        allTransitions.Add(transition);
    }

    public class LocomotionState : State2<Actor>
    {
        public LocomotionState(Actor actor) : base(actor)
        {
            //Do setup for the state here
        }
        public override void OnEnter()
        {
            Debug.Log(" entered Locomotion");
        }
        public override void OnExit() { }
        public override void OnUpdate() { }
    }

    public class AnotherState : State2<Actor>
    {
        public AnotherState(Actor actor) : base(actor)
        {
            //Do setup for the state here
        }
        public override void OnEnter()
        {
            Debug.Log(" entered AnotherState");
        }
        public override void OnExit() { }
        public override void OnUpdate() { }
    }
}

