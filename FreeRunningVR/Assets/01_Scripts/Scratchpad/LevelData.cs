using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ObjectData/LevelData", order = 2)]
public class LevelData : ScriptableObject
{
    public StartLevel startLevel;
    public FinishLevel finishLevel;
}
