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

    [SerializeField] private float jumnpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;

    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private InputActionProperty JumpInputSource;



    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    public bool grounded;


    [SerializeField] private Transform orientation;
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
        InputManager.Instance.playerInputActions.Walking.TryToJump.canceled += TryToJump;
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
            Jump();

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

    private void Jump()
    {
        // reset y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumnpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }

    private void TryToJump(InputAction.CallbackContext context)
    {
        //rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        float input = context.ReadValue<float>();
        Debug.Log(input);
        if (readyToJump && grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    Vector2 ReadJoystickInput(InputAction.CallbackContext context)
    {
        Vector2 stickInput = context.ReadValue<Vector2>();
        return stickInput;
    }
}
