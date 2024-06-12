using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class TargetManager : MonoBehaviour
{
    public event Action OnAllTargetsHit;

    public float number = 0.3f;
    [SerializeField] private PistolManager pistolManager;
    [SerializeField] private RespawnManagerQuick respawnManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private HighScoreManager highScoreManager;
    [SerializeField] private List<Target> activeTargets;
    [SerializeField] private List<Target> deactiveTargets;
    [SerializeField] private Target targetPrefab;

    [SerializeField] private TextMeshProUGUI targetUI;
    private List<float> scores = new List<float>();
    private List<TargetTransform> beginTargets = new List<TargetTransform>();
    private List<Target> activeTargetsToBeRemoved = new List<Target>();
    private List<Target> deactiveTargetsToBeRemoved = new List<Target>();
    private List<Vector3> targetTransformValues;
    private List<List<Vector3>> targetTransform;


    // Start is called before the first frame update
    void Start()
    {
        SetBeginningTargets();
        highScoreManager.OnRestartLevel += RespawnTargets;
        respawnManager.OnPlayerEnterdCheckPoint += UpdateDeactiveTargetList;
        //beginTargets = activeTargets;
        SetEvents();
        RespawnTargets();
    }

    private void SetBeginningTargets()
    {
        foreach (Target target in activeTargets)
        {
            TargetTransform targetTrans = new TargetTransform(target.transform.position, target.transform.rotation);

            beginTargets.Add(targetTrans);

        }
    }

    private void RespawnTargets()
    {
        // emptying activeTargets
        foreach (Target target in activeTargets)
        {
            target.OnAnimtionIsDone -= RemoveTarget;
            activeTargetsToBeRemoved.Add(target);
        }

        foreach (Target target in activeTargetsToBeRemoved)
        {
            activeTargets.Remove(target);
            Destroy(target.gameObject);
        }

        activeTargetsToBeRemoved.Clear();

        foreach (Target target in deactiveTargets)
        {
            target.OnAnimtionIsDone -= RemoveTarget;
            deactiveTargetsToBeRemoved.Add(target);
        }

        foreach (Target target in deactiveTargetsToBeRemoved)
        {
            deactiveTargets.Remove(target);
            Destroy(target.gameObject);
        }

        deactiveTargetsToBeRemoved.Clear();


        foreach (TargetTransform targetTrans in beginTargets)
        {
            Target newTarget = Instantiate(targetPrefab, targetTrans.position, targetTrans.rotation);
            activeTargets.Add(newTarget);
            newTarget.OnAnimtionIsDone += RemoveTarget;
        }

    }

    private void UpdateDeactiveTargetList()
    {

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
        Debug.Log("begin removeTarget");
        activeTargets.Remove(hitTarget);
        deactiveTargets.Add(hitTarget);
        hitTarget.gameObject.SetActive(false);

        if (activeTargets.Count == 0)
        {
            float avarage = Calculations.calculateAvarage(scores);
            //targetUI.text = avarage.ToString();
            OnAllTargetsHit?.Invoke();
        }

        Debug.Log("Ended removeTarget");

    }

    private void procesHit(Target target, Vector3 hitPoint)
    {
        Target newTarget = GetTarget(target);
        if (newTarget == null) return;
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

    public float SendAccuracy()
    {
        float avarage;
        if (scores.Count != 0)
        {
            avarage = Calculations.calculateAvarage(scores);
        }
        else
        {
            avarage = 0.0f;
        }

        return avarage;
    }

    public int SendHitObjects()
    {
        return deactiveTargets.Count;
    }
}
