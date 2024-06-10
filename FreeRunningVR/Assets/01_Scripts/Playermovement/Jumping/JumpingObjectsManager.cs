using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObjectsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<JumpObject> jumpingObjects = new List<JumpObject>();
    [SerializeField] private List<JumpObject> jumpingObjectsDeactive = new List<JumpObject>();

    [SerializeField] private float timerDuration;
    [SerializeField] private float maxObjectCount;

    private PlayerData playerData;
    private JumpColliderDetection upperDetection;
    private JumpColliderDetection lowerDetection;
    private JumpColliderDetection LandingDetection;
    private JumpObject currentJumpObject;
    private JumpObject previousHitJumpObject;

    private Transform upperBody;
    private Transform orientation;
    private Transform lowerBody;
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
            //jumpObject.GetCollider();
        }
    }

    private void Update()
    {
        if (mayUpdate == false) return;

        if (upperDetection != null)
        {
            upperDetection.OnUpdate();
            LandingDetection.OnUpdate();
            lowerDetection.OnUpdate();
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
        }
        else if (playerData.IsGoingUp == 2)
        {
            foreach (JumpObject _object in jumpingObjects)
            {
                _object.TurnToCollider();
            }
        }
        else
        {
            foreach (JumpObject _object in jumpingObjects)
            {
                _object.TurnToCollider();
            }
        }
    }

    private void FixedUpdate()
    {
        if (upperDetection == null) return;

        if (playerData.IsGoingUp == 1)
        {


        }
        else if (playerData.IsGoingUp == 2)
        {

            LandingDetection.OnFixedUpdate();
        }
        else
        {
            upperDetection.OnFixedUpdate();
            lowerDetection.OnFixedUpdate();

        }
    }

    private void TurnObjectToCollider()
    {
        //Debug.Log("turn to Collider");
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
        lowerBody = playerData.playerGameObjects.lowerBody;
        orientation = playerData.playerGameObjects.orientation;
        CreateBodyDetection();
    }

    private void ProcesUpperDection(JumpObject jumpObject)
    {
        RemoveJumpObjectFromList(jumpObject);

        if (previousHitJumpObject != null && jumpObject != previousHitJumpObject)
        {
            AddJumpObjectToList(previousHitJumpObject);
        }
        previousHitJumpObject = jumpObject;
        Debug.Log("remove");
    }
    private void ProcesLowerDection(JumpObject jumpObject)
    {
        if(jumpingObjects.Count > maxObjectCount)
        {
            jumpingObjects[0].OnPlayerEnterTrigger -= PlayerIsInObject;
            //jumpingObjects[0].TurnToCollider();
            jumpingObjects.Remove(jumpingObjects[0]);
        }

        jumpingObjects.Add(jumpObject);
        jumpObject.OnPlayerEnterTrigger += PlayerIsInObject;

    }
    private void ProcesLandingDection(JumpObject jumpObject)
    {
        //AddJumpObjectToList(jumpObject);
        //jumpingObjects.Add(jumpObject);
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

    private void CreateBodyDetection()
    {
        upperDetection = new JumpColliderDetection(upperBody, orientation, 0.8f);
        upperDetection.OnJumpObjectHit += ProcesUpperDection;

        LandingDetection = new JumpColliderDetection(upperBody, orientation, -0.8f);
        LandingDetection.OnJumpObjectHit += ProcesLandingDection;

        lowerDetection = new JumpColliderDetection(lowerBody, orientation, 0);
        lowerDetection.OnJumpObjectHit += ProcesLowerDection;

        mayUpdate = true;
    }








}
