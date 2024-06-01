using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MiniGame", fileName = "MiniGameObjects", order = 0)]
public class MiniGameObjects : ScriptableObject
{
    public List<Target> targets;

    public GameObject enterTriggerObject;
    public GameObject exitTriggerObject;

}
