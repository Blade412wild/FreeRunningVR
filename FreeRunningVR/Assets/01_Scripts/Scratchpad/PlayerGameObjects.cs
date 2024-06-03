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
    public Transform upperBody;

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
    public AudioSource audioSource;

    [Header("Models")]
    public GameObject rightHandModel;
    public GameObject rightHandPhysics;
    public GameObject rightGun;

    public GameObject leftHandModel;
    public GameObject leftHandPhysics;
    public GameObject leftGun;
}
