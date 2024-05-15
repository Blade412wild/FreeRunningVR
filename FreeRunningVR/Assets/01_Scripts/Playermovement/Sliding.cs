using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sliding : MonoBehaviour
{
    [SerializeField] private Walking walking;
    [SerializeField] private Rigidbody headRB;
    [SerializeField] private Rigidbody bodyRB;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private float camOriginalYPos = 0;
    [SerializeField] private float camSlidingDecrease = -0.65f;
    [SerializeField] private float maxSlideDuration = 2.0f;
    [SerializeField] private float SlideForce = 5.0f;
    [SerializeField] private bool ForceSlide;


    private Timer1 slidingTimer;
    private float headYVelocity;
    private bool updateSlideTimer = false;
    private bool isSliding = false;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();

        if (walking.grounded == true && isSliding == false)
        {
            //Debug.Log("may check");
            CheckIfSliding();
        }
    }

    private void UpdateTimer()
    {
        if (updateSlideTimer)
        {
            slidingTimer.OnUpdate();
        }
    }

    private void CheckIfSliding()
    {
        headYVelocity = headRB.velocity.y;
        //Debug.Log("HeadVelocity : " + headYVelocity);
        if (headYVelocity <= -1 && headYVelocity >= -2 && bodyRB.velocity.magnitude > walking.StartMoveSpeed + 2.0f || ForceSlide == true)
        {
            if (slidingTimer == null)
            {
                slidingTimer = new Timer1(maxSlideDuration);
                slidingTimer.OnTimerIsDone += StopSliding;
            }
            Slide();
            updateSlideTimer = true;
        }
    }
    private void Slide()
    {
        cameraOffset.localPosition = new Vector3(cameraOffset.localPosition.x, camSlidingDecrease, cameraOffset.localPosition.z);
        bodyRB.AddForce(walking.moveDirection.normalized * SlideForce, ForceMode.Impulse);
    }

    private void StopSliding()
    {
        updateSlideTimer = false;
        slidingTimer.ResetTimer();
        cameraOffset.localPosition = new Vector3(cameraOffset.localPosition.x, camOriginalYPos, cameraOffset.localPosition.z);
        isSliding = false;
        ForceSlide = false;

    }
}
