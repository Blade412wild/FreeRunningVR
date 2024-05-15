using System.Collections;
using System.Collections.Generic;
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

}
