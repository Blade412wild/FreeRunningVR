using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ObjectData/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("playerData")]
    public bool grounded;
    public float WalkSpeed;
    public float RunSpeed;
    public PlayerGameObjects playerGameObjects;
    public object previousState;
    public List<Collider> Colliders;

    [Header("playerInstace")]
    public GameObject PlayerPrefab;
}
