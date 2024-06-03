using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObjectsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<JumpObject> jumpingObjects;
    [SerializeField] private List<JumpObject> jumpingObjectsDeactive;

    [SerializeField] private float timerDuration;

    private PlayerData playerData;
    private UpperBodyColliderDetection upperDection;
    private JumpObject currentJumpObject;
    private JumpObject previousHitJumpObject;

    private Transform upperBody;
    private Transform orientation;
    private bool DidPutToTrigger = false;
    private Timer1 timer;
    private bool timerMayUpdate = false;
    private bool mayUpdate = false;
    private Color newColor;

    private void Start()
    {
        newColor = Color.red;
        timer = new Timer1(timerDuration);
        timer.OnTimerIsDone += TurnObjectToCollider;
        gameManager.OnSpawnPlayerDone += GetPlayerData;

        foreach (JumpObject jumpObject in jumpingObjects)
        {
            jumpObject.GetCollider();
            jumpObject.OnPlayerEnterTrigger += PlayerIsInObject;
        }
    }

    private void Update()
    {
        if (mayUpdate == false) return;

        if (upperDection != null)
        {
            upperDection.OnUpdate();
        }

        if (timerMayUpdate)
        {
            timer.OnUpdate();
        }

        if (playerData.IsGoingUp == 1)
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

    private void FixedUpdate()
    {
        if (upperDection == null) return;
        upperDection.OnFixedUpdate();
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

    private void GetPlayerData()
    {
        playerData = gameManager.playerData;
        upperBody = playerData.playerGameObjects.upperBody;
        orientation = playerData.playerGameObjects.orientation;
        CreateUpperbodyDetection();
    }

    private void ProcesHit(JumpObject jumpObject)
    {
        RemoveJumpObjectFromList(jumpObject);

        if (previousHitJumpObject != null && jumpObject != previousHitJumpObject)
        {
            AddJumpObjectToList(previousHitJumpObject);
        }
        previousHitJumpObject = jumpObject;
        Debug.Log("remove");
    }

    private void AddJumpObjectToList(JumpObject previousHitObject)
    {      
        if (!jumpingObjects.Contains(previousHitObject))
        {
            jumpingObjects.Add(previousHitObject);
        }

        jumpingObjectsDeactive.Remove(previousHitObject);
    }

    private void RemoveJumpObjectFromList(JumpObject jumpObject)
    {
      
        jumpingObjects.Remove(jumpObject);
        jumpingObjectsDeactive.Add(jumpObject);

    }

    private void CreateUpperbodyDetection()
    {
        upperDection = new UpperBodyColliderDetection(upperBody, orientation);
        upperDection.OnJumpObjectHit += ProcesHit;
        mayUpdate = true;
    }








}
