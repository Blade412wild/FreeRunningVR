using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Rigidbody leftHandRB;
    [SerializeField] private Rigidbody rightHandRB;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform orientation;
    [SerializeField] private CapsuleCollider bodyCollider;

    [SerializeField] private Transform handsMiddleTrans;
    [SerializeField] private Transform CenterBodyPrefabTrans;

    [Header("Running")]
    [SerializeField] private float HandsSameSiteThreshold = 0.1f;
    [SerializeField] private float VelocityThreshold = 1.5f;

    [SerializeField] private int RunningTimerDuration = 1;

    private List<float> leftHandRecordings = new List<float>();
    private List<float> rightHandRecordings = new List<float>();

    private Vector3 Centerbody;
    Timer1 RunningTimer;

    // Start is called before the first frame update
    void Start()
    {
        RunningTimer = new Timer1(RunningTimerDuration);
        RunningTimer.OnTimerIsDone += TimerIsDone;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfRunning();
    }

    private void CheckIfRunning()
    {
        // calculating Hands MiddlePoint
        Centerbody = bodyCollider.transform.TransformPoint(bodyCollider.center);
        Vector3 NewPos = (leftHandTransform.position + rightHandTransform.position) / 2;
        NewPos = new Vector3(Centerbody.x, NewPos.y, NewPos.z);
        handsMiddleTrans.position = NewPos;

        CenterBodyPrefabTrans.position = Centerbody;

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
                Debug.Log("LH : " + avarageLH + " | RH : " + avarageRH);
            }
            //if (LHVelocity.z < 0.5f && RHVelocity.z < 0.5f) return;
            //RunningTimer.ResetTimer();
            //Debug.Log("Running");
        }
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

    private void TimerIsDone()
    {
        Debug.Log("Timer Is Done");
    }
}
