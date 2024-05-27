using UnityEngine;
using System;
using System.Collections.Generic;

public class FinishLevel : MonoBehaviour
{
    public event Action OnFinishLevel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    private PlayerData playerData;
    private List<Collider> colliders;
    private bool firstTrigger = true;
    private bool mayFinish = false;

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        colliders = playerData.Colliders;
        gameManager.OnSpawnPlayerDone += SetColliders;
        levelManager.OnDataInputDone += ResetFinish;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!firstTrigger) return;
        bool isPlayer = CheckIfColliderPlayer(other);
        if (isPlayer)
        {
            mayFinish = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!mayFinish) return;
        firstTrigger = false;
        mayFinish = false;
        OnFinishLevel?.Invoke();
    }

    private bool CheckIfColliderPlayer(Collider other)
    {
        foreach (Collider collider in colliders)
        {
            if (collider == other) return true;
        }
        return false;
    }

    private void SetColliders()
    {
        colliders = playerData.Colliders;
    }

    private void ResetFinish()
    {
        Debug.Log("ResetFinish");
        firstTrigger = true;
    }
}
