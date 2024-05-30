using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ChangeHandsManager
{
    //right hand
    private GameObject rightHand;
    private GameObject rightRB;
    private GameObject rightGun;

    // left hand
    private GameObject leftHand;
    private GameObject leftRB;
    private GameObject leftGun;

    public ChangeHandsManager(PlayerData playerdata)
    {
        SetGameObjects(playerdata);
    }
    
    private void SetGameObjects(PlayerData playerdata)
    {
        rightHand = playerdata.playerGameObjects.rightHandModel;
        rightRB = playerdata.playerGameObjects.rightHandPhysics;
        rightGun = playerdata.playerGameObjects.rightGun;

        leftHand = playerdata.playerGameObjects.leftHandModel;
        leftRB = playerdata.playerGameObjects.leftHandPhysics;
        leftGun = playerdata.playerGameObjects.leftGun;
    }

    public void ChangeHands(int hand)
    {
        if(hand == 0)
        {
            Debug.Log("left");
        }
        else
        {
            Debug.Log("right");
        }
    }

}

