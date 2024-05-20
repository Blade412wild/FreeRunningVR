using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : State
{
    [Header("Scripts")]
    [SerializeField] private PlayerStateHandler stateHandler;
    private GameManager gameManager;
    private PlayerData playerData;

    [SerializeField] private float camOriginalYPos = 0;
    [SerializeField] private float camSlidingDecrease = -0.65f;
    [SerializeField] private float maxSlideDuration = 1.3f;
    [SerializeField] private float SlideForce = 5.0f;
    [SerializeField] private bool ForceSlide;
    [SerializeField] private float maxSlideSpeed = 10;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;
    public float StartMoveSpeed;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask Body;
    public bool grounded { get; private set; }

    [Header("SlopeHandeling")]
    [SerializeField] private float maxSlopeAngle;
    private RaycastHit slopeHit;

    //GameObjects
    private Transform orientation;
    private CapsuleCollider bodyCollider;

    private float horizontalInput;
    private float verticalInput;
    public bool exitingSlope;

    public Vector3 moveDirection { get; private set; }
    private Rigidbody rb;
    //GameObjects
    private Rigidbody headRB;
    private Rigidbody bodyRB;
    private Transform cameraOffset;

    private Timer1 slidingTimer;
    private float headYVelocity;
    private bool updateSlideTimer = false;
    private bool isSliding = false;
    private float previousPosY;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = stateHandler.gameManager;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
    }
    public override void OnEnter()
    {
        Debug.Log(" entered Sliding");
        InputManager.Instance.playerInputActions.Walking.Enable();
        if (slidingTimer == null)
        {
            slidingTimer = new Timer1(maxSlideDuration);
            slidingTimer.OnTimerIsDone += StopSliding;
        }

        Slide();
    }
    private void SetGameObjects()
    {
        headRB = playerData.playerGameObjects.headRB;
        bodyRB = playerData.playerGameObjects.bodyRB;
        cameraOffset = playerData.playerGameObjects.orientation.parent;
        orientation = playerData.playerGameObjects.orientation;
        bodyCollider = playerData.playerGameObjects.bodyCollider;

        rb = playerData.playerGameObjects.bodyRB;
        rb.freezeRotation = true;
    }

    private void UpdateTimer()
    {
        slidingTimer.OnUpdate();
    }

    private void Slide()
    {
        cameraOffset.localPosition = new Vector3(cameraOffset.localPosition.x, camSlidingDecrease, cameraOffset.localPosition.z);
        bodyRB.AddForce(moveDirection.normalized * SlideForce, ForceMode.Impulse);
    }

    private void StopSliding()
    {
        cameraOffset.localPosition = new Vector3(cameraOffset.localPosition.x, camOriginalYPos, cameraOffset.localPosition.z);
        slidingTimer.ResetTimer();
        Controller.SwitchState(typeof(RunningState));
    }
    public override void OnUpdate()
    {
        if (slidingTimer != null)
        {
            UpdateTimer();
        }
        float speedY =  CalculateSpeed();
        if (speedY > 0.6f)
        {
            StopSliding();
        }
        MyInput();

        //SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    public override void OnFixedUpdate()
    {
        CheckGround();
        Walk();
        Slide();
    }

    private void MyInput()
    {
        Vector2 stickInput = InputManager.Instance.playerInputActions.Walking.MoveVR.ReadValue<Vector2>();
        horizontalInput = stickInput.x;
        verticalInput = stickInput.y;
    }


    private void Walk()
    {
        // calculate movement direction
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 10f, ForceMode.Force);
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);
        //// in air
        //else if (!grounded)
        //    rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);

        // on slop
        rb.useGravity = !OnSlope(); // needs to be better, cost double calculation
    }

    // jummping & Walking 
    private void CheckGround()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, rayLenght, whatIsGround);
        grounded = CheckIfGroundIsGround();
        playerData.grounded = grounded;

    }
    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVelocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

            if (flatVelocity.magnitude > maxSlideSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * maxSlideSpeed;
                rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
            }
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

    private bool CheckIfGroundIsGround()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLenght = bodyCollider.height / 2 - bodyCollider.radius + 0.5f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLenght, whatIsGround);
        return hasHit;
    }
    private bool OnSlope()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLenght = bodyCollider.height / 2 + 0.3f;

        if (Physics.Raycast(start, Vector3.down, out slopeHit, rayLenght))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public override void OnExit()
    {
        slidingTimer.ResetTimer();
    }
    private float CalculateSpeed()
    {
        float speed;
        float currenYpos = orientation.position.y;
        speed = (currenYpos - previousPosY) / Time.deltaTime;
        previousPosY = currenYpos;
        return speed;
    }

}
