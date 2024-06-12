using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTransform
{
    public Vector3 position;
    public Quaternion rotation;

    public TargetTransform(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
