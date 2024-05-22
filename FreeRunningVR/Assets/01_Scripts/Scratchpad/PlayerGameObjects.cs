using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerGameObjects : MonoBehaviour
{
    [Header("Transforms")]
    public Transform playerHead;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public Transform frontTrans;
    public Transform backTrans;
    public Transform handsMiddleTrans;
    public Transform centerBodyPrefabTSrans;
    public Transform orientation;
    public Transform CameraOffset;
    public Transform XROrigin;
    public XROrigin XROrigin2;



    [Header("RigidBody")]
    public Rigidbody headRB;
    public Rigidbody bodyRB;
    public Rigidbody leftHandRB;
    public Rigidbody rightHandRB;

    [Header("Joints")]
    public ConfigurableJoint headJoint;
    public ConfigurableJoint rightHandJoint;
    public ConfigurableJoint leftHandJoint;

    [Header("Collider")]
    public CapsuleCollider bodyCollider;
    public float maxHeight;
}
