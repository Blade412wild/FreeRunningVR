using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetLevel : MonoBehaviour
{
    public List<Target> targetsList;

    public List<Target> GetTargets()
    {
        Target[] targets = GetComponentsInChildren<Target>();
        targetsList = targets.ToList<Target>();
        return targetsList;
    }

   
}
