using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class TargetManager : MonoBehaviour
{

    public float number = 0.3f;
    [SerializeField] private PistolManager pistolManager;
    [SerializeField] private List<Target> activeTargets;
    [SerializeField] private List<Target> deactiveTargets;


    [SerializeField] private TextMeshProUGUI targetUI;
    private List<float> scores = new List<float>();






    // Start is called before the first frame update
    void Start()
    {
        SetEvents();
    }

    private void SetEvents()
    {
        pistolManager.OnPistolHasHitTarget += procesHit;

        foreach (Target target in activeTargets)
        {
            target.OnAnimtionIsDone += RemoveTarget;
        }
    }

    private void RemoveTarget(Target hitTarget)
    {
        activeTargets.Remove(hitTarget);
        deactiveTargets.Add(hitTarget);
        hitTarget.gameObject.SetActive(false);

        if (activeTargets.Count == 0)
        {
            float avarage = Calculations.calculateAvarage(scores);
            targetUI.text = avarage.ToString();
        }
    }

    private void procesHit(Target target, Vector3 hitPoint)
    {
        Target newTarget = GetTarget(target);
        newTarget.AudioSource.Play();
        newTarget.Animator.SetTrigger("hit");
        float Score = CalculateScore(target, hitPoint);
        newTarget.Accuracy = Score;
        scores.Add(Score);

        if (activeTargets.Count == 0)
        {
            float avarage = Calculations.calculateAvarage(scores);
            targetUI.text = avarage.ToString();
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