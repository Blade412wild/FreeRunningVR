using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airMultiplier;

    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
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

    // jummping & Walking 
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


}
