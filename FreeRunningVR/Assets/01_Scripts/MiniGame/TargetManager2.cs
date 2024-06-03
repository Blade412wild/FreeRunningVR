using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetManager2
{
    public event Action OnAllTargetsHit;
    public event Action OnBuildLevel;

    public float number = 0.3f;
    private PistolManager pistolManager;
    private List<Target> targetsLevel;
    private List<Target> activeTargets;
    private List<Target> deactiveTargets = new List<Target>();
    private List<TargetLevel> targetLevels;
    private Transform spawnLocation;


    private TextMeshProUGUI targetUI;
    private List<float> scores = new List<float>();

    public TargetManager2(PistolManager pistolManager, Transform spawnLocation, List<TargetLevel> targetLevels)
    {
        this.pistolManager = pistolManager;
        this.spawnLocation = spawnLocation;
        this.targetLevels = targetLevels;

        pistolManager.OnPistolHasHitTarget += procesHit;
    }
    private void AddEventsLisnteners()
    {
        foreach (Target target in activeTargets)
        {
            target.OnAnimtionIsDone += RemoveTarget;
            Debug.Log(" target " + target.name);
        }
    }

    public void BuildLevel()
    {
        var newLevel = GameObject.Instantiate(targetLevels[0], spawnLocation);
        activeTargets = newLevel.GetTargets();
        AddEventsLisnteners();
        OnBuildLevel?.Invoke();


    }

    public void ResetLevel()
    {
        GameObject.Instantiate(targetLevels[0], spawnLocation);
        AddEventsLisnteners();
    }


    private void RemoveTarget(Target hitTarget)
    {
        hitTarget.OnAnimtionIsDone -= RemoveTarget;
        hitTarget.gameObject.SetActive(false);

        if (activeTargets.Count == 0)
        {
            float avarage = Calculations.calculateAvarage(scores);
            //targetUI.text = avarage.ToString();
            OnAllTargetsHit?.Invoke();
        }
    }

    private void procesHit(Target target, Vector3 hitPoint)
    {
        Target newTarget = GetTarget(target);
        if (newTarget == null) return;

        activeTargets.Remove(newTarget);
        deactiveTargets.Add(newTarget);

        newTarget.AudioSource.Play();
        newTarget.Animator.SetTrigger("hit");
        float Score = CalculateScore(target, hitPoint);
        newTarget.Accuracy = Score;
        scores.Add(Score);

        if (activeTargets.Count == 0)
        {
            float avarage = Calculations.calculateAvarage(scores);
            //targetUI.text = avarage.ToString();
        }

    }

    private Target GetTarget(Target hittarget)
    {
        foreach (Target target in activeTargets)
        {
            if (target == hittarget)
            {
                return target;
            }
        }

        return null;
    }

    private float CalculateScore(Target target, Vector3 hitPoint)
    {
        float maxDistance = target.Radius;
        float distance = Vector3.Distance(target.Center, hitPoint);
        float score = Calculations.CalculateNewValueInNewScale(distance, maxDistance, 0.0f, 50.0f, 100.0f);

        return score;
    }
}
