using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;

public class Running : MonoBehaviour
{
    public enum Fases { Walking, Running };
    public enum HandFases { NextToBody, Front, Back }
    [Header("Scritps")]
    public Walking walking;

    [Header("GameObjects")] 
    [SerializeField] private Rigidbody leftHandRB;
    [SerializeField] private Rigidbody rightHandRB;
    [SerializeField] private Rigidbody bodyRB;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform orientation;
    [SerializeField] private CapsuleCollider bodyCollider;

    [SerializeField] private Transform frontTrans;
    [SerializeField] private Transform backTrans;

    [SerializeField] private Transform handsMiddleTrans;
    [SerializeField] private Transform centerBodyPrefabTrans;

    [Header("Running")]
    public Fases PlayerFase;
    public HandFases handFase;
    [SerializeField] private float maxRunningSpeed = 15.0f;
    //[SerializeField] private float handsSameSiteThreshold = 0.1f;
    //[SerializeField] private float velocityThreshold = 1.5f;
    [SerializeField] private float speedThreshold = 1.5f;
    [SerializeField] private float maxDistance = 0.3f;
    [SerializeField] private float time;
    [SerializeField] private float handsNexToBodyAngleMin = 0.015f;
    [SerializeField] private float handsNexToBodyAngleMax = 0.025f;
    [SerializeField] private float handNextToBodyTime = 0.3f;
    [SerializeField] private float handNextToBodySpeedBuffer = 0.6f;
    [SerializeField] private float HandSpeedTest = 3f;

    Timer1 RunningTimer;
    Timer1 handsBesideBodyTimer;
    private int tick = 0;
    private int currentLeftHandSide;
    private int previousLeftHandSide;
    private bool firstTick = true;



    private float distanceFrontBack;
    private float RHFrontDistance;
    private float RHBackDistance;
    private float newRHSpeed;
    private float BodySpeed;
    private float RHSpeed;



    [SerializeField] private float RunningTimerDuration = 1.5f;


    private List<float> leftHandRecordings = new List<float>();
    private List<float> rightHandRecordings = new List<float>();

    private Vector3 Centerbody;


    // Start is called before the first frame update
    void Start()
    {
        PlayerFase = Fases.Walking;
        distanceFrontBack = Vector3.Distance(frontTrans.position, backTrans.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatingTimers();
        UpdatingMovementSpeed();
        CalculatingBodyparts();
        CheckIfRunning();
        CalculateHandDistance();
    }

    private void UpdatingMovementSpeed()
    {
        if (PlayerFase == Fases.Running)
        {
            walking.moveSpeed = maxRunningSpeed;
        }
        else if (PlayerFase == Fases.Walking)
        {
            walking.moveSpeed = walking.StartMoveSpeed;
        }
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

        if (PlayerFase != Fases.Running) return;

        if (yAngle > handsNexToBodyAngleMin && yAngle < handsNexToBodyAngleMax)
        {
            if (newRHSpeed > -handNextToBodySpeedBuffer && newRHSpeed < handNextToBodySpeedBuffer)
            {
                if (handsBesideBodyTimer == null)
                {
                    handsBesideBodyTimer = new Timer1(handNextToBodyTime);
                    handsBesideBodyTimer.OnTimerIsDone += SetPlayerFaseToWalking2;
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

    private void CheckIfRunning()
    {

        // getting Body and Hand speeds
        float LHSpeed = leftHandRB.velocity.magnitude;
        RHSpeed = rightHandRB.velocity.magnitude;
        BodySpeed = bodyRB.velocity.magnitude;

        // normalizing HandSpeeds
        float newLHSpeed = LHSpeed - BodySpeed;
        newRHSpeed = RHSpeed - BodySpeed;

        CreateTimer();

        if (RHFrontDistance > RHBackDistance)
        {
            //Debug.Log("LeftHandBack");
            currentLeftHandSide = 0;
            handFase = HandFases.Back;

            if (firstTick == true)
            {
                previousLeftHandSide = currentLeftHandSide;
                firstTick = false;
            }

        }
        if (RHFrontDistance < RHBackDistance)
        {
            //Debug.Log("LeftHandFront");
            currentLeftHandSide = 1;
            handFase = HandFases.Front;


            if (firstTick == true)
            {
                previousLeftHandSide = currentLeftHandSide;
                firstTick = false;
            }
        }


        // if the hand hasn;t changed side it won't reset te timer;
        if (currentLeftHandSide != previousLeftHandSide)
        {
            //Debug.Log("tick");
            tick++;
            RunningTimer.ResetTimer();
            PlayerFase = Fases.Running;
            previousLeftHandSide = currentLeftHandSide;
        }
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
        if (RunningTimer == null)
        {
            RunningTimer = new Timer1(RunningTimerDuration);
            RunningTimer.OnTimerIsDone += SetPlayerFaseToWalking;
        }
    }
    private void SetPlayerFaseToWalking()
    {
        if (PlayerFase == Fases.Walking) return;
        Debug.Log("Timer Is Done");
        PlayerFase = Fases.Walking;
    }
    private void SetPlayerFaseToWalking2()
    {
        if (PlayerFase == Fases.Walking) return;
        //Debug.Log("Timer Hands Is Done");
        PlayerFase = Fases.Walking;
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
