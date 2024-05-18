using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public event Action OnStartLevel;
    [SerializeField] private GameManager gameManager;
    private PlayerData playerData;
    private List<Collider> colliders;
    private bool firstTrigger = true;

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        colliders = playerData.Colliders;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = CheckIfColliderPlayer(other);
        if (firstTrigger && isPlayer != null)
        {
            OnStartLevel?.Invoke();
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
}
