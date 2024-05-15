using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckJumping : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Walking walking;
    PlayerData playerData;

    [Header("Jump")]
    [SerializeField] private float smallJumnpForce;
    [SerializeField] private float bigJumnpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;

    [SerializeField] private float JumpVelocityMin = 2.0f;
    [SerializeField] private float JumpTravelDistanceMin = 0.2f;
    public bool grounded;

    [Header("GroundCheck")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;

    [Header("GameObjects")]
    private Rigidbody leftHandRB;
    private Rigidbody rightHandRB;
    private Transform leftHandTransform;
    private Transform rightHandTransform;
    private Transform orientation;

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
        playerData = gameManager.ObjectData.Read<PlayerData>("playerData");
        SetGameObjects();

        rb = GetComponent<Rigidbody>();
        InputManager.Instance.playerInputActions.Walking.TryToJump.canceled += DicideHowToJump;
        InputManager.Instance.playerInputActions.Walking.TryToJump.started += SetJumpData;
    }

    private void SetGameObjects()
    {
        leftHandRB = playerData.playerGameObjects.leftHandRB;
        rightHandRB = playerData.playerGameObjects.rightHandRB;
        leftHandTransform = playerData.playerGameObjects.leftHandTransform;
        rightHandTransform = playerData.playerGameObjects.rightHandTransform;
        orientation = playerData.playerGameObjects.orientation;
    }

    // jummping & Walking 
    private void CheckGround()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    // jumping 
    private void SmallJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * smallJumnpForce, ForceMode.Impulse);
    }

    //jumping
    private void BigJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * bigJumnpForce, ForceMode.Impulse);
    }

    // jumping 
    private void resetJump()
    {
        readyToJump = true;
        walking.exitingSlope = false;
    }

    //jumping
    private void SetJumpData(InputAction.CallbackContext context)
    {
        leftHandInitialPos = leftHandTransform.position;
        rightHandInitialPos = rightHandTransform.position;
    }

    //jumping
    private void DicideHowToJump(InputAction.CallbackContext context)
    {
        
        if (!readyToJump && !grounded) return;

        Vector3 leftHandEndPos = leftHandTransform.position;
        Vector3 rightHandEndPos = rightHandTransform.position;

        // check if hands are movedup
        if (leftHandEndPos.y < leftHandInitialPos.y || rightHandEndPos.y < rightHandInitialPos.y)
        {
            walking.exitingSlope = true;
            SmallJump();
            readyToJump = false;
            Invoke(nameof(resetJump), jumpCooldown);
            
        }

        float leftHandDistanceTraveled = leftHandEndPos.y - leftHandInitialPos.y;
        float rightHandDistaneTraveled = rightHandEndPos.y - rightHandInitialPos.y;

        // check if if hands have travelded Enough
        if (leftHandDistanceTraveled < 0.2f && rightHandDistaneTraveled < 0.2f) return;

        Vector3 leftHandEndVel = leftHandRB.velocity;
        Vector3 rightHandEndVel = rightHandRB.velocity;

        // check if velocity is enough
        if (leftHandEndVel.y < 2.0f && rightHandEndVel.y < 3.0f) return;
        walking.exitingSlope = true;
        BigJump();
        readyToJump = false;
        Invoke(nameof(resetJump), jumpCooldown);
    }

}
