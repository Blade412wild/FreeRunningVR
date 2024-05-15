using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{

    private StateMachine stateMachine;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new StateMachine(this, GetComponents<State>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
