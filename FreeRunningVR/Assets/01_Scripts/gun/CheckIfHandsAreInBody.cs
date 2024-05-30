using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfHandsAreInBody : IUpdateable
{
    public event Action<bool> OnLeftHandChanged;
    public event Action<bool> OnRightHandChanged;

    private Transform orientation;
    private Transform rightHand;
    private Transform leftHand;

    private bool isRightHandInArea = false;
    private bool isLeftHandInArea = false;

    private float radius;



    public CheckIfHandsAreInBody(PlayerData playerData)
    {
        orientation = playerData.playerGameObjects.orientation;
        radius = playerData.playerGameObjects.bodyCollider.radius;
        leftHand = playerData.playerGameObjects.leftHandTransform;
        rightHand = playerData.playerGameObjects.rightHandTransform;
        Debug.Log(orientation);
        Debug.Log(radius);
    }


    // Update is called once per frame
    public void OnUpdate()
    {
        // right hand enters
        if (Calculations.CalculateCapsuleCollision(orientation, rightHand, 0.5f, radius) == true && isRightHandInArea == false)
        {
            isRightHandInArea = true;
            OnRightHandChanged?.Invoke(isRightHandInArea);
        }

        // right hand exits
        if (Calculations.CalculateCapsuleCollision(orientation, rightHand, 0.5f, radius) == false && isRightHandInArea == true)
        {
            isRightHandInArea = false;
            OnRightHandChanged?.Invoke(isRightHandInArea);
        }

        // left hand enters
        if (Calculations.CalculateCapsuleCollision(orientation, leftHand, 0.5f, radius) == true && isLeftHandInArea == false)
        {
            isLeftHandInArea = true;
            OnLeftHandChanged?.Invoke(isLeftHandInArea);
        }

        // left hand exits
        if (Calculations.CalculateCapsuleCollision(orientation, leftHand, 0.5f, radius) == false && isLeftHandInArea == true)
        {
            isLeftHandInArea = false;
            OnLeftHandChanged?.Invoke(isLeftHandInArea);
        }
    }
}