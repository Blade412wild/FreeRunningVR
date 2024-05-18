using UnityEngine;
using System;
using System.Collections.Generic;

public class FinishLevel : MonoBehaviour
{
    public event Action OnFinishLevel;
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;
    private List<Collider> colliders;
    private bool firstTrigger = true;

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        colliders = playerData.Colliders;
        gameManager.OnSpawnPlayerDone += SetColliders;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!firstTrigger) return;
        bool isPlayer = CheckIfColliderPlayer(other);
        if (isPlayer)
        {
            OnFinishLevel?.Invoke();
            firstTrigger = false;
        }

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
}
