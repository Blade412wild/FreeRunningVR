using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData
{
    private LevelManager levelManager;
    private TargetManager targetManager;
    private HighscoreRequirements highscoreRequirements;
    private string[] grades = { "S", "A+", "A", "B+", "B", "C+", "C", "F" };

    public GetPlayerData(HighscoreRequirements highscoreRequirements, LevelManager levelManager, TargetManager targetManager)
    {
        this.levelManager = levelManager;
        this.targetManager = targetManager;
        this.highscoreRequirements = highscoreRequirements;
    }

    public Vector4 GetData()
    {
        //X = Grade, Y= time, Z = hits, W = accuracy
        Vector4 playerData = new Vector4();

        //playerData.y = levelManager.SendTime();
       // playerData.z = targetManager.SendHitObjects();
        //playerData.w = targetManager.SendAccuracy();
        //playerData.x = CalculateScore(playerData);

        playerData.y = 50;
        playerData.z = 2;
        playerData.w = 90;
        playerData.x = 1;

        return playerData;
    }

    private int CalculateScore(Vector4 playerData)
    {
        Debug.Log("===============");
        Debug.Log("Calculating Grade");
        DateTime startTime = DateTime.Now;
        int grade = 0;

        for (int i = 0; i < grades.Length; i++)
        {
            Debug.Log("------------");
            if (i != grades.Length - 1)
            {
                Debug.Log("Check Rank : " + grades[i]);
                if (CheckIfTheRequirementsAreMet(playerData.y, highscoreRequirements.Requirements[i].y, true) == false) continue;
                Debug.Log("time is good");
                if (CheckIfTheRequirementsAreMet(playerData.z, highscoreRequirements.Requirements[i].z, false) == false) continue;
                Debug.Log("hits are good");
                if (CheckIfTheRequirementsAreMet(playerData.w, highscoreRequirements.Requirements[i].w, false) == false) continue;
                Debug.Log("accuracy is good");
            }

            Debug.Log("Player Got Rank : " + grades[i]);
            // Giving grade
            //playerData.x = i;
            DateTime endTime = DateTime.Now;
            TimeSpan timePast = endTime - startTime;
            Debug.Log(String.Format("Time Spent: {0} Milliseconds", timePast.TotalMilliseconds));
            grade = i;
            break;
        }
        return grade;
    }

    private bool CheckIfTheRequirementsAreMet(float playerscore, float requiredScore, bool reverse)
    {
        // reverse moet true zijn wanneer de playerdata kleiner moet zijn dan je requirements
        if (reverse == true)
        {
            if (playerscore >= requiredScore) return false;
            return true;
        }
        else
        {
            if (playerscore < requiredScore) return false;
            return true;

        }
    }



}
