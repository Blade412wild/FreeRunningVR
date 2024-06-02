using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public IState fromState;
    public IState toState;
    public Func<bool> condition;
    public Transition(IState fromState, IState toState, Func<bool> condition)
    {
        this.fromState = fromState;
        this.toState = toState;
        this.condition = condition;
    }
    public bool Evalutate()
    {
        return condition();
    }
}
