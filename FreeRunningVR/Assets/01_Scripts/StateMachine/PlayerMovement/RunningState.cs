using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : State
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CheckRunning checkRunning;
    [SerializeField] private CheckSliding checkSliding;
    private PlayerData playerData;


    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;
    public float StartMoveSpeed;


    [Header("SlopeHandeling")]
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private bool exitingSlope;
    private RaycastHit slopeHit;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask Body;
    public bool grounded { get; private set; }
    // Input
    private float horizontalInput;
    private float verticalInput;

    //GameObjects
    private Transform orientation;
    private CapsuleCollider bodyCollider;




    public Vector3 moveDirection { get; private set; }
    private Rigidbody rb;


    void Start()
    {
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
        moveSpeed = playerData.RunSpeed;
    }
    public override void OnEnter()
    {
        InputManager.Instance.playerInputActions.Walking.Enable();
    }
    public override void OnExit()
    {
        InputManager.Instance.playerInputActions.Walking.Disable();
    }

    public override void OnUpdate()
    {
        MyInput();
        if (!checkRunning.IsStillRunning())
        {
            Controller.SwitchState(typeof(WalkingState));
        }

        if (checkSliding.IsSliding())
        {
            Controller.SwitchState(typeof(SlidingState));
        }


        SpeedControl();

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
    }



    private void SetGameObjects()
    {
        orientation = playerData.playerGameObjects.orientation;
        bodyCollider = playerData.playerGameObjects.bodyCollider;

        rb = playerData.playerGameObjects.bodyRB;
        rb.freezeRotation = true;

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

            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
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
}
