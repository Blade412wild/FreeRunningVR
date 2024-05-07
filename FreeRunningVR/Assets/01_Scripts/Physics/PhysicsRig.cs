using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform playerHead;
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("Joints")]
    [SerializeField] private ConfigurableJoint headJoint;
    [SerializeField] private ConfigurableJoint rightHandJoint;
    [SerializeField] private ConfigurableJoint leftHandJoint;

    [Header("Collider")]
    [SerializeField] private CapsuleCollider bodyCollider;


    [SerializeField] private float bodyHeightMin = 0.5f;
    [SerializeField] private float bodyHeightMax = 2.0f;

    private void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;
        headJoint.targetRotation = playerHead.localRotation;

    }


}
