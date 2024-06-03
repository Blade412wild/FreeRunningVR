using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObjectsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<JumpObject> jumpingObjects;
    [SerializeField] private float timerDuration;

    private PlayerData playerData;
    private bool DidPutToTrigger = false;
    private Timer1 timer;
    private bool timerMayUpdate = false;
    private JumpObject currentJumpObject;

    private void Start()
    {
        timer = new Timer1(timerDuration);
        timer.OnTimerIsDone += TurnObjectToCollider;
        playerData = gameManager.playerData;

        foreach (JumpObject jumpObject in jumpingObjects)
        {
            jumpObject.GetCollider();
            jumpObject.OnPlayerEnterTrigger += PlayerIsInObject;
        }
    }

    private void Update()
    {
        if (timerMayUpdate)
        {
            timer.OnUpdate();
        }

        if (playerData.IsGoingUp == 1 && DidPutToTrigger == false)
        {
            foreach (JumpObject _object in jumpingObjects)
            {
                _object.TurnToTrigger();
            }
            //DidPutToTrigger = true;

        }
        else if (playerData.IsGoingUp == 2)
        {
            foreach (JumpObject _object in jumpingObjects)
            {
                _object.TurnToCollider();
            }
            DidPutToTrigger = false;

        }
        else
        {
            foreach (JumpObject _object in jumpingObjects)
            {
                _object.TurnToCollider();
            }
            DidPutToTrigger = false;
        }


    }

    private void TurnObjectToCollider()
    {
        Debug.Log("turn to Collider");
        currentJumpObject.TurnToCollider();
        timerMayUpdate = false;
        timer.ResetTimer();
    }

    private void PlayerIsInObject(JumpObject objectCollider, Collider otherCollider)
    {
        if (otherCollider.GetType() == typeof(SphereCollider))
        {
            objectCollider.TurnToCollider();
        }
        else
        {
            timerMayUpdate = true;
        }
        currentJumpObject = objectCollider;
    }



    private void PlayerIsOutOfTrigger()
    {

    }

    private void SetPlayerData()
    {

    }






}
