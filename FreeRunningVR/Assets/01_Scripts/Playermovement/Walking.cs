using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        Debug.Log("walking : " + InputManager.Instance.playerInputActions);
        InputManager.Instance.playerInputActions.Walking.Enable();
        InputManager.Instance.playerInputActions.Walking.Jump.performed += Jump;

        //PlayerInputActions actions = new PlayerInputActions();
        //actions.Walking.Jump.performed += Jump;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
