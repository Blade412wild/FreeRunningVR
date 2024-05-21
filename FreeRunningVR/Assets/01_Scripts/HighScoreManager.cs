using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public LevelManager levelManager;
    [SerializeField] private string Name;
    private string fullPath;
    private string fileName;

    private void Start()
    {
        levelManager.OnEndLevel += TryToSavePlayerData;
    }


    public void TryToSavePlayerData()
    {
        Debug.Log(" try to Save");
        string path = GetPath();
        //checkIfThere is room on the highscore board else return;
        CheckIfThereIsAFile(path);

        PlayerDataStruct playerData = GetPlayerData();
        SaveData(playerData, path);
        CheckIfThereIsAFile(path);

    }


    public string GetPath()
    {
        if (Application.isPlaying)
        {
            return fullPath = Application.dataPath + fileName + ".txt";
        }
        else
        {
            return fullPath = Application.persistentDataPath + fileName + ".txt";
        }
    }

    private void SaveData(PlayerDataStruct playerData, string path)
    {
        StreamWriter streamWriter = new StreamWriter(path, false);
        streamWriter.WriteLine(JsonUtility.ToJson(playerData, true));
        streamWriter.Close();
        streamWriter.Dispose();
    }

    private PlayerDataStruct GetPlayerData()
    {
        PlayerDataStruct playerData = new PlayerDataStruct();
        playerData._name = Name;
        playerData._time = levelManager.PlayerGameStopWatch.currentTime;
        playerData._rank = 1;

        return playerData;
    }

    private void CheckIfThereIsAFile(string path)
    {
        if (File.Exists(path) == false) return;
        StreamReader reader = new StreamReader(path);
        PlayerDataStruct playerData = JsonUtility.FromJson<PlayerDataStruct>(reader.ReadToEnd());
        reader.Close();
        reader.Dispose();
        Debug.Log(" Name : " + playerData._name + " | Time : " + playerData._time);
    }

    private void CheckHighScoreBoard()
    {

    }


}

struct PlayerDataStruct
{
    public string _name;
    public float _time;
    public int _rank;
}

struct LevelDataStruct
{
    public int _level;
    public List<PlayerDataStruct> _highScores;


}
