using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigJumpState : State
{
    private enum JumpingState { Up, Down };
    [Header("Scripts")]
    [SerializeField] private PlayerStateHandler stateHandler;
    private GameManager gameManager;
    private PlayerData playerData;

    [Header("Jump")]
    [SerializeField] private float smallJumnpForce;
    [SerializeField] private float bigJumnpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float PlayerDownFallMass;
    [SerializeField] private float downForce;
    [SerializeField] private JumpingState jumpingState;

    private bool readyToJump = true;
    private float startingMass;


    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private float groundDrag;
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


    void Start()
    {
        gameManager = stateHandler.gameManager;
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();
        //moveSpeed = playerData.WalkSpeed;

    }
    public override void OnEnter()
    {
        jumpingState = JumpingState.Up;
        InputManager.Instance.playerInputActions.Walking.Enable();
        Debug.Log(" entered : Jumping");

        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * bigJumnpForce, ForceMode.Impulse);
        startingMass = rb.mass;
    }
    public override void OnExit()
    {
        InputManager.Instance.playerInputActions.Walking.Disable();
        rb.mass = startingMass;
    }

    public override void OnUpdate()
    {
        MyInput();
        // wall Running Check?

        if (rb.velocity.y < 0)
        {
            rb.mass = PlayerDownFallMass;
            jumpingState = JumpingState.Down;
        }

        SpeedControl();
        rb.drag = 0;
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

        rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f /**airMultiplier*/, ForceMode.Force);
        if(jumpingState == JumpingState.Down)
        {
            rb.AddForce(Vector3.down * downForce, ForceMode.Force);
        }
    }

    // jummping & Walking 
    private void CheckGround()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, rayLenght, whatIsGround);
        if (jumpingState == JumpingState.Up) return;
        grounded = CheckIfGroundIsGround();
        if (grounded && rb.velocity.y >= 0)
        {
            Controller.SwitchState(typeof(RunningState));
        }
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
