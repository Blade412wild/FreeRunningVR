using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public bool FreezeBodyCollider = false;
    [SerializeField] private PlayerStateHandler playerStateHandler;
    private GameManager gameManager;


    [Header("Transforms")]
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("Joints")]
    [SerializeField] private ConfigurableJoint rightHandJoint;
    [SerializeField] private ConfigurableJoint leftHandJoint;

    [Header("Collider")]
    [SerializeField] private CapsuleCollider bodyCollider;
    [SerializeField] private Transform headCollider;


    [SerializeField] private float bodyHeightMin = 0.5f;
    [SerializeField] private float bodyHeightMax = 2.0f;


    private void Start()
    {
        gameManager = playerStateHandler.gameManager;
        gameManager.playerData.maxHeight = bodyHeightMax;
    }

    private void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, gameManager.playerData.maxHeight);
        if (!FreezeBodyCollider)
        {
            bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);
        }

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headCollider.position = playerHead.position;
    }


}
