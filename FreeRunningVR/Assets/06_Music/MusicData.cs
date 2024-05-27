using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Data/MusicData", order = 1)]

public class MusicData : ScriptableObject
{
    public AudioClip startSound;
    public AudioClip levelMusic;
}
