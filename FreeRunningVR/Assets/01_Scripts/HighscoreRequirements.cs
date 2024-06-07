using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData", fileName = "HighscoreRequirements", order = 1)]
public class HighscoreRequirements : ScriptableObject
{
    [Header("X = Grade, Y= time, Z = hits, W = accuracy")]
    [Header("S=0, A+=1, A=2, B+=3, B=4, C+=5, C=6, F=7")] 
    public Vector4[] Requirements;

}

