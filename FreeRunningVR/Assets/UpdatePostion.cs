using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpdatePostion : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] private bool ignoreX;
    [SerializeField] private bool ignoreY;
    [SerializeField] private bool ignoreZ;

    private Vector3 newTarget;


    // Update is called once per frame
    void Update()
    {
        if (ignoreY)
        {
            newTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = newTarget;
        }
        else
        {
            transform.position = target.position;

        }
    }
}
