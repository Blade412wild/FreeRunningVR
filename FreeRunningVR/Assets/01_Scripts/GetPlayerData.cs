using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerData
{
    private LevelManager levelManager;
    private TargetManager targetManager;

    public GetPlayerData(LevelManager levelManager, TargetManager targetManager)
    {
        this.levelManager = levelManager;
        this.targetManager = targetManager;
    }

    public Vector4 GetData()
    {
        //X = Grade, Y= time, Z = hits, W = accuracy
        Vector4 playerData = new Vector4();

        playerData.y = levelManager.SendTime();
        playerData.z = targetManager.SendHitObjects();
        playerData.w = targetManager.SendAccuracy();


        return playerData;
    }

}
