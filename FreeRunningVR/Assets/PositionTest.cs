using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour
{

    public Vector3 Pos;
    public bool LocalPos;

    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (LocalPos)
        {
            transform.localPosition = Pos;
        }
        else
        {
            transform.position = Pos;
        }



    }
}
