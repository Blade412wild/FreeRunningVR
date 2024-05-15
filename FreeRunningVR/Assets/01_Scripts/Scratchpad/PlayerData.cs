using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerData", menuName = "ObjectData")]
public class PlayerData : ScriptableObject
{
    [Header("playerData")]
    public bool grounded;
    public PlayerGameObjects playerGameObjects;
    public object previousState;

    
}
