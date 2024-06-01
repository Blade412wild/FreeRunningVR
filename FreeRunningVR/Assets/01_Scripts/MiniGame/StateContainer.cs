using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM", fileName = "StateDataContainer", order = 0)]
public class StateContainer : ScriptableObject
{
    public int[] states;
}
