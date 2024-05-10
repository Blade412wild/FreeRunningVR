using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;

    [SerializeField] private float smallJumnpForce;
    [SerializeField] private float bigJumnpForce;

    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private InputActionProperty JumpInputSource;

    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Jump")]
    [SerializeField] private float JumpVelocityMin = 2.0f;
    [SerializeField] private float JumpTravelDistanceMin = 0.2f;
    public bool grounded;

    [Header("GameObjects")]
    [SerializeField] private Rigidbody leftHandRB;
    [SerializeField] private Rigidbody rightHandRB;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform orientation;

    private List<float> leftHandRecordings = new List<float>();
    private List<float> rightHandRecordings = new List<float>();

    private Vector3 leftHandInitialPos;
    private Vector3 rightHandInitialPos;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        InputManager.Instance.playerInputActions.Walking.Enable();
        InputManager.Instance.playerInputActions.Walking.TryToJump.canceled += DicideHowToJump;
        InputManager.Instance.playerInputActions.Walking.TryToJump.started += SetJumpData;

    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
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

    private void FixedUpdate()
    {
        Walk();
        CheckGround();
    }

    private void MyInput()
    {
        Vector2 stickInput = InputManager.Instance.playerInputActions.Walking.MoveVR.ReadValue<Vector2>();
        horizontalInput = stickInput.x;
        verticalInput = stickInput.y;
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            SmallJump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }



    private void Walk()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        if (grounded)
            rb.AddForce(moveDirection * moveSpeed * 10.0f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);

    }

    private void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);


        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void SmallJump()
    {
        Debug.Log("SmallJump");
        // reset y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * smallJumnpForce, ForceMode.Impulse);
    }

    private void BigJump()
    {
        Debug.Log("biggJump");
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * bigJumnpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }

    private void SetJumpData(InputAction.CallbackContext context)
    {
        leftHandInitialPos = leftHandTransform.position;
        rightHandInitialPos = rightHandTransform.position;
    }


    private void DicideHowToJump(InputAction.CallbackContext context)
    {
        if (!readyToJump && !grounded) return;

        Vector3 leftHandEndPos = leftHandTransform.position;
        Vector3 rightHandEndPos = rightHandTransform.position;

        if (leftHandEndPos.y < leftHandInitialPos.y || rightHandEndPos.y < rightHandInitialPos.y)
        {
            SmallJump();
            readyToJump = false;
            Invoke(nameof(resetJump), jumpCooldown);
        }

        float leftHandDistanceTraveled = leftHandEndPos.y - leftHandInitialPos.y;
        float rightHandDistaneTraveled = rightHandEndPos.y - rightHandInitialPos.y;

        //leftHandRecordings.Add(leftHandDistanceTraveled);
        //rightHandRecordings.Add(rightHandDistaneTraveled);
        //Debug.Log("lefthand Avarage : " + calculateAvarage(leftHandRecordings));
        //Debug.Log("righthand Avarage : " + calculateAvarage(rightHandRecordings));

        if (leftHandDistanceTraveled < 0.2f && rightHandDistaneTraveled < 0.2f) return;

        Vector3 leftHandEndVel = leftHandRB.velocity;
        Vector3 rightHandEndVel = rightHandRB.velocity;

        if (leftHandEndVel.y < 2.0f && rightHandEndVel.y < 3.0f) return;

        //leftHandRecordings.Add(lefthandEndVel.y);
        //rightHandRecordings.Add(rightHandEndVel.y);
        //Debug.Log("lefthand Avarage : " + calculateAvarage(leftHandRecordings));
        //Debug.Log("righthand Avarage : " + calculateAvarage(rightHandRecordings));

        BigJump();
        readyToJump = false;
        Invoke(nameof(resetJump), jumpCooldown);
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

    Vector2 ReadJoystickInput(InputAction.CallbackContext context)
    {
        Vector2 stickInput = context.ReadValue<Vector2>();
        return stickInput;
    }
}
