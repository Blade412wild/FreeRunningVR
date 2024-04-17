using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    public event Action OnJumpPressed;
    private Rigidbody rb;

    private Vector2 input = new Vector2(0,0);
    private Vector3 direction = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputManager.Instance.playerInputActions.Walking.Enable();
        InputManager.Instance.playerInputActions.Walking.TryToJump.performed += TryToJump;
        InputManager.Instance.playerInputActions.Walking.Move.performed += ReadInput;
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void TryToJump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void ReadInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Walk()
    {
        direction = new Vector3(input.x, 0, input.y);
        transform.position += direction * Time.deltaTime;
        
    }
}
