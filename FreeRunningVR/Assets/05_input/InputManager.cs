using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public PlayerInputActions playerInputActions;

    [SerializeField] private Jumping jumping;

    private void Awake()
    {
        if (Instance != null && Instance == this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        playerInputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwitchToJumping()
    {
        jumping.Jump();
    }
}
