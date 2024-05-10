using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTargetPos;

    void Update()
    {
        transform.position = cameraTargetPos.position;
        transform.rotation = cameraTargetPos.rotation;
    }
}
