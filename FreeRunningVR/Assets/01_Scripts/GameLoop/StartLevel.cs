using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    public event Action OnStartLevel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    private PlayerData playerData;
    private AudioSource audioSource;
    private List<Collider> colliders;
    private bool firstTrigger = true;
    

    private void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        colliders = playerData.Colliders;
        audioSource = GetComponent<AudioSource>();

        gameManager.OnSpawnPlayerDone += SetColliders;
        levelManager.OnDataInputDone += ResetStart;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!firstTrigger) return;
        audioSource.Play();

        bool isPlayer = CheckIfColliderPlayer(other);
        if (isPlayer)
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

    private void SetColliders()
    {
        colliders = playerData.Colliders;
    }

    private void ResetStart()
    {
        Debug.Log("Reset Start");
        firstTrigger = true;
    }
}
