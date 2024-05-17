using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;

public class CheckRunning : MonoBehaviour
{
    public enum HandFases { NextToBody, Front, Back }

    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    public Walking walking;
    PlayerData playerData;

    [Header("Tick")]
    [SerializeField] private float time;
    [SerializeField] private float handNextToBodyTime = 0.3f;
    [SerializeField] private float RunningTimerDuration = 1.5f;
    private Timer1 RunningTimer;
    private int tick = 0;
    private bool firstTick = true;
    private int currentLeftHandSide;
    private int previousLeftHandSide;

    [Header("HandsBeside Body")]
    public HandFases handFase;
    [SerializeField] private float speedThreshold = 1.5f;
    [SerializeField] private float handsNexToBodyAngleMin = 0.015f;
    [SerializeField] private float handsNexToBodyAngleMax = 0.025f;
    [SerializeField] private float handNextToBodySpeedBuffer = 0.6f;
    private Timer1 handsBesideBodyTimer;

    //GameObjects 
    private Rigidbody leftHandRB;
    private Rigidbody rightHandRB;
    private Rigidbody bodyRB;
    private CapsuleCollider bodyCollider;
    private Transform leftHandTransform;
    private Transform rightHandTransform;
    private Transform orientation;
    private Transform frontTrans;
    private Transform backTrans;
    private Transform handsMiddleTrans;
    private Transform centerBodyPrefabTrans;

    //distances
    private float distanceFrontBack;
    private float RHFrontDistance;
    private float RHBackDistance;

    //speeds 
    private float newRHSpeed;
    private float BodySpeed;
    private float RHSpeed;

    private bool IsStillRunningBool = true;

    private Vector3 Centerbody;


    // Start is called before the first frame update
    void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();

        distanceFrontBack = Vector3.Distance(frontTrans.position, backTrans.position);
    }

    public void OnUpdate()
    {
        UpdatingTimers();
        CalculatingBodyparts();
        CheckTick();
        CalculateHandDistance();
    }

    public bool IsRunning()
    {
        CalculatingBodyparts();
        bool isRunning = CheckIfBeginningToRun();
        return isRunning;
    }

    public bool IsStillRunning()
    {
        UpdatingTimers();
        CalculatingBodyparts();
        CheckTick();
        CalculateHandDistance();
        if (!IsStillRunningBool)
        {
            ResetChecks();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SetGameObjects()
    {
        leftHandRB = playerData.playerGameObjects.leftHandRB;
        rightHandRB = playerData.playerGameObjects.rightHandRB;
        bodyRB = playerData.playerGameObjects.bodyRB;
        leftHandTransform = playerData.playerGameObjects.leftHandTransform;
        rightHandTransform = playerData.playerGameObjects.rightHandTransform;
        orientation = playerData.playerGameObjects.orientation;
        bodyCollider = playerData.playerGameObjects.bodyCollider;

        frontTrans = playerData.playerGameObjects.frontTrans;
        backTrans = playerData.playerGameObjects.backTrans;

        handsMiddleTrans = playerData.playerGameObjects.handsMiddleTrans;
        centerBodyPrefabTrans = playerData.playerGameObjects.centerBodyPrefabTSrans;
    }

    public void ResetChecks()
    {
        if (RunningTimer != null)
        {
            RunningTimer.ResetTimer();
        }

        if (handsBesideBodyTimer != null)
        {
            handsBesideBodyTimer.ResetTimer();
        }

        IsStillRunningBool = true;
    }

    private bool CheckIfBeginningToRun()
    {
        bool isHandMoving = IsHandMoving();
        return isHandMoving;
    }


    private void UpdatingTimers()
    {
        if (RunningTimer != null)
        {
            RunningTimer.OnUpdate();
            time = RunningTimer.currentTime;

        }

        if (handsBesideBodyTimer != null)
        {
            handsBesideBodyTimer.OnUpdate();
        }
    }

    private void CalculateHandDistance()
    {
        RHFrontDistance = Vector3.Distance(frontTrans.position, rightHandTransform.position);
        RHBackDistance = Vector3.Distance(backTrans.position, rightHandTransform.position);

        float yAngle = CalculateAngle(RHBackDistance, RHFrontDistance, distanceFrontBack);

        Debug.DrawLine(frontTrans.position, rightHandTransform.position, Color.white);
        Debug.DrawLine(backTrans.position, rightHandTransform.position, Color.white);
        //Debug.Log("yAngle : " + yAngle);


        if (yAngle > handsNexToBodyAngleMin && yAngle < handsNexToBodyAngleMax)
        {
            if (newRHSpeed > -handNextToBodySpeedBuffer && newRHSpeed < handNextToBodySpeedBuffer)
            {
                if (handsBesideBodyTimer == null)
                {
                    handsBesideBodyTimer = new Timer1(handNextToBodyTime);
                    handsBesideBodyTimer.OnTimerIsDone += SetRunningBoolToFalse;
                }

                handFase = HandFases.NextToBody;
            }
        }
        else
        {
            if (handsBesideBodyTimer == null) return;
            handsBesideBodyTimer.ResetTimer();
        }

    }
    private bool IsHandMoving()
    {
        RHFrontDistance = Vector3.Distance(frontTrans.position, rightHandTransform.position);
        RHBackDistance = Vector3.Distance(backTrans.position, rightHandTransform.position);

        float yAngle = CalculateAngle(RHBackDistance, RHFrontDistance, distanceFrontBack);

        Debug.DrawLine(frontTrans.position, rightHandTransform.position, Color.white);
        Debug.DrawLine(backTrans.position, rightHandTransform.position, Color.white);

        if (yAngle < handsNexToBodyAngleMin || yAngle > handsNexToBodyAngleMax)
        {
            CalculateRHSpeedNormalized();
            if (newRHSpeed > 1.0f && rightHandRB.velocity.y > 1.0)
            {
                return true;
            }
        }

        return false;

    }

    private void CalculateRHSpeedNormalized()
    {
        // getting Body and Hand speeds
        RHSpeed = rightHandRB.velocity.magnitude;
        BodySpeed = bodyRB.velocity.magnitude;
        newRHSpeed = RHSpeed - BodySpeed;
    }

    private void CheckTick() // eerst moet nog gecheckt worden of er wel snelheid is.
    {

        CalculateRHSpeedNormalized();

        if (RunningTimer == null)
        {
            CreateTimer();
        }

        if (RHFrontDistance > RHBackDistance)
        {
            //Debug.Log("LeftHandBack");
            currentLeftHandSide = 0;
            handFase = HandFases.Back;

            if (firstTick == true)
            {
                SetFirstTick();
            }
        }

        if (RHFrontDistance < RHBackDistance)
        {
            //Debug.Log("LeftHandFront");
            currentLeftHandSide = 1;
            handFase = HandFases.Front;

            if (firstTick == true)
            {
                SetFirstTick();
            }
        }

        // if the hand hasn;t changed side it won't reset te timer;
        if (currentLeftHandSide != previousLeftHandSide)
        {
            tick++;
            RunningTimer.ResetTimer();
            previousLeftHandSide = currentLeftHandSide;
        }
    }

    private void SetFirstTick()
    {
        previousLeftHandSide = currentLeftHandSide;
        firstTick = false;
    }

    private void CalculatingBodyparts()
    {
        Centerbody = bodyCollider.transform.TransformPoint(bodyCollider.center);

        // calulating Body Pos & Rot
        centerBodyPrefabTrans.position = Centerbody;
        centerBodyPrefabTrans.rotation = Quaternion.Euler(centerBodyPrefabTrans.rotation.eulerAngles.x, orientation.rotation.eulerAngles.y, centerBodyPrefabTrans.rotation.eulerAngles.z);

        // calculating Hands MiddlePoint
        Vector3 NewPos = (leftHandTransform.position + rightHandTransform.position) / 2;
        //NewPos = new Vector3(CenterBodyPrefabTrans.localPosition.z, NewPos.y, NewPos.z);
        handsMiddleTrans.position = NewPos;
    }

    private float calculateAvarage(List<float> list)
    {
        float allNumbers = 0;

        foreach (var number in list)
        {
            allNumbers += number;
        }

        float average = allNumbers / list.Count;
        return average;
    }
    private void CreateTimer()
    {
        RunningTimer = new Timer1(RunningTimerDuration);
        RunningTimer.OnTimerIsDone += SetRunningBoolToFalse;
    }
    private void SetRunningBoolToFalse()
    {
        IsStillRunningBool = false;
    }
    private float CalculateAngle(float a, float b, float c)
    {
        // c2 = a2 + b2 -2ab * cos(y)
        // X =  -2ab
        // A = c2 - (a2 + b2)
        // cos(y) = A / X

        float angle;

        float a2 = a * a;
        float b2 = b * b;
        float c2 = c * c;

        float A = c2 - (a2 + b2);
        float X = -2 * a * b;

        float yAngle = Mathf.Acos(A / X);

        return yAngle;
    }
}
