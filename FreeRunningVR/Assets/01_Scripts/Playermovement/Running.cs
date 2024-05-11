using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;

public class Running : MonoBehaviour
{
    public enum Fases { Walking, Running };
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
    [SerializeField] private float maxRunningSpeed = 10.0f;
    [SerializeField] private float handsSameSiteThreshold = 0.1f;
    [SerializeField] private float velocityThreshold = 1.5f;
    [SerializeField] private float speedThreshold = 1.5f;
    [SerializeField] private float maxDistance = 0.3f;
    [SerializeField] private float time;

    private int tick = 0;
    private int currentLeftHandSide;
    private int previousLeftHandSide;
    private bool firstTick = true;

    [SerializeField] private int RunningTimerDuration = 1;


    private List<float> leftHandRecordings = new List<float>();
    private List<float> rightHandRecordings = new List<float>();

    private Vector3 Centerbody;
    Timer1 RunningTimer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerFase = Fases.Walking;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CurrentSide : " + currentLeftHandSide + " | PreviousSide : " +  previousLeftHandSide);
        if (RunningTimer != null)
        {
            RunningTimer.OnUpdate();
            time = RunningTimer.currentTime;
        }

        if (PlayerFase == Fases.Running)
        {
            walking.moveSpeed = maxRunningSpeed;
        }
        else if (PlayerFase == Fases.Walking)
        {
            walking.moveSpeed = walking.StartMoveSpeed;
        }


        CalculatingBodyparts();
        CheckIfRunning2();
        //CheckIfRunningVelocity();

    }

    // based on the middle point of my hands and the speed of the controllers
    private void CheckIfRunning2()
    {

        // getting Body and Hand speeds
        float LHSpeed = leftHandRB.velocity.magnitude;
        float RHSpeed = rightHandRB.velocity.magnitude;
        float BodySpeed = bodyRB.velocity.magnitude;

        // normalizing HandSpeeds
        float newLHSpeed = LHSpeed - BodySpeed;
        float newRHSpeed = RHSpeed - BodySpeed;

        if (newLHSpeed < speedThreshold || newRHSpeed < speedThreshold) return;

        // creating Timer
        CreateTimer();

        float RHFrontDistance = Vector3.Distance(frontTrans.position, rightHandTransform.position);
        float RHBackDistance = Vector3.Distance(backTrans.position, rightHandTransform.position);

        if (RHFrontDistance > RHBackDistance)
        {
            //Debug.Log("LeftHandBack");
            currentLeftHandSide = 0;

            if(firstTick == true)
            {
                previousLeftHandSide = currentLeftHandSide;
                firstTick = false;
            }
        }

        if (RHFrontDistance < RHBackDistance)
        {
            //Debug.Log("LeftHandFront");
            currentLeftHandSide = 1;

            if (firstTick == true)
            {
                previousLeftHandSide = currentLeftHandSide;
                firstTick = false;
            }
        }

       




        // if the hand hasn;t changed side it won't reset te timer;
        if (currentLeftHandSide != previousLeftHandSide)
        {
            Debug.Log("tick");
            tick++;
            RunningTimer.ResetTimer();
            PlayerFase = Fases.Running;
            previousLeftHandSide = currentLeftHandSide;
        }







        //RunningTimer.ResetTimer();
        //Debug.Log("IsRunning");





        //calculate the coeffient
        //float LHCoeffient = LHVelocity.y / LHVelocity.z;
        //float RHCoeffient = RHVelocity.y / RHVelocity.z;

        //leftHandRecordings.Add(LHSpeed);
        //rightHandRecordings.Add(RHSpeed);

        //float avarageLH = calculateAvarage(leftHandRecordings);
        //float avarageRH = calculateAvarage(rightHandRecordings);

        //Debug.Log("LH : " + avarageLH + " | RH : " + avarageRH);
    }
    public Vector2 FindHighestAndLowest(List<float> floatList)
    {

        float highest = floatList[0];
        float lowest = floatList[0];

        foreach (float num in floatList)
        {
            if (num > highest)
            {
                highest = num;
            }
            else if (num < lowest)
            {
                lowest = num;
            }
        }

        Vector2 HighandLow = new Vector2(highest, lowest);
        return HighandLow;
    }

    private bool CalculateHandsSpeeds()
    {
        bool enoughSpeed = false;

        return enoughSpeed;

    }
    private void CheckIfRunningVelocity()
    {

        // Calculate handsPos Based on Center of Body
        // LH = Left Hand RH = Right Hand
        float deltaCenterLHPosZ = Centerbody.z - leftHandTransform.position.z;
        float deltaCenterRHPosZ = Centerbody.z - rightHandTransform.position.z;


        // check if both hands are at the front or at the back
        //if (deltaLeftHandPosZ > HandsSameSiteTreshold && deltaRightHandPosZ > HandsSameSiteTreshold || deltaLeftHandPosZ < -HandsSameSiteTreshold  && deltaRightHandPosZ < -HandsSameSiteTreshold)
        //{
        //    Debug.Log("in front");
        //}
        //else
        //{
        //    Debug.Log("in back");
        //}
        //if (deltaCenterLHPosZ > HandsSameSiteTreshold && deltaCenterRHPosZ > HandsSameSiteTreshold) return;

        if (deltaCenterLHPosZ > 0 && deltaCenterRHPosZ < 0 || deltaCenterLHPosZ < 0 && deltaCenterRHPosZ > 0)
        {
            CalulateVelocity();


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
            RunningTimer.OnTimerIsDone += TimerIsDone;
        }
    }

    private void TimerIsDone()
    {
        Debug.Log("Timer Is Done");
        PlayerFase = Fases.Walking;
    }

    private void CalulateVelocity()
    {
        Vector3 LHVelocity = leftHandRB.velocity;
        Vector3 RHVelocity = rightHandRB.velocity;

        //Vector3 minVelocity = new Vector3(1, 1, 1);

        //calculate the coeffient
        float LHCoeffient = LHVelocity.y / LHVelocity.z;
        float RHCoeffient = RHVelocity.y / RHVelocity.z;

        leftHandRecordings.Add(LHCoeffient);
        rightHandRecordings.Add(RHCoeffient);

        float avarageLH = calculateAvarage(leftHandRecordings);
        float avarageRH = calculateAvarage(rightHandRecordings);
        if (avarageLH < -1 && avarageRH > 1 || avarageLH > 1 && avarageRH < -1)
        {
            //Debug.Log("LH : " + avarageLH + " | RH : " + avarageRH);
        }
        //if (LHVelocity.z < 0.5f && RHVelocity.z < 0.5f) return;
        //RunningTimer.ResetTimer();
        //Debug.Log("Running");
    }

    private void CalulateSpeed()
    {
        float LHSpeed = leftHandRB.velocity.magnitude;
        float RHSpeed = rightHandRB.velocity.magnitude;

        //Vector3 minVelocity = new Vector3(1, 1, 1);



        leftHandRecordings.Add(LHSpeed);
        rightHandRecordings.Add(RHSpeed);

        float avarageLH = calculateAvarage(leftHandRecordings);
        float avarageRH = calculateAvarage(rightHandRecordings);
        Debug.Log("LH : " + avarageLH + " | RH : " + avarageRH);

        //if (avarageLH < -1 && avarageRH > 1 || avarageLH > 1 && avarageRH < -1)
        //{
        //    Debug.Log("LH : " + avarageLH + " | RH : " + avarageRH);
        //}
        //if (LHVelocity.z < 0.5f && RHVelocity.z < 0.5f) return;
        //RunningTimer.ResetTimer();
        //Debug.Log("Running");
    }

    
}
