using System;
using UnityEngine;

public class JumpColliderDetection
{
    public event Action<JumpObject> OnJumpObjectHit;

    [SerializeField] private PlayerStateHandler playerStateHandler;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Transform orientation;
    private Transform upperBodyTrans;
    private Collider currentJumpObject;
    private Collider previousJumpObject;
    private float offsetY;

    public Vector3 direction = new Vector3(0, 1, 0);

    public JumpColliderDetection(Transform upperBodyTrans, Transform orientation, float OffsetY)
    {
        this.upperBodyTrans = upperBodyTrans;
        this.orientation = orientation;
        this.offsetY = OffsetY;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        CalculateDirection();

    }

    public void OnFixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(upperBodyTrans.position, direction, out hit, 5.0f))
        {
            Debug.DrawRay(upperBodyTrans.position, direction * hit.distance, Color.green);

            currentJumpObject = hit.collider;

            if (currentJumpObject != previousJumpObject)
            {
                if (hit.collider.gameObject.TryGetComponent<JumpObject>(out JumpObject jumpObject))
                {
                    OnJumpObjectHit.Invoke(jumpObject);
                }
            }
            else
            {
                previousJumpObject = currentJumpObject;
            }


            previousJumpObject = currentJumpObject;

        }
        else
        {
            Debug.DrawRay(upperBodyTrans.position, direction * 1000, Color.red);
        }
    }
    private void CalculateDirection()
    {
        Vector2 stickInput = InputManager.Instance.playerInputActions.Walking.MoveVR.ReadValue<Vector2>();
        horizontalInput = stickInput.x;
        verticalInput = stickInput.y;

        direction = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        direction = new Vector3(direction.x, offsetY, direction.z);
    }

    private void GetOrientatiom()
    {
        orientation = playerStateHandler.gameManager.playerData.playerGameObjects.orientation;
    }

}