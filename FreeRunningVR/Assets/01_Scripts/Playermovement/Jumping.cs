using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    [SerializeField] private Walking1 walking;

    [SerializeField] private float jumnpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InputManager.Instance.playerInputActions.Walking.TryToJump.canceled += TryToJump;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

    }


    private void resetJump()
    {
        readyToJump = true;
    }

    private void TryToJump(InputAction.CallbackContext context)
    {
        //rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        float input = context.ReadValue<float>();
        Debug.Log(input);
        if (readyToJump && walking.grounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    private void Jump()
    {
        // reset y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumnpForce, ForceMode.Impulse);
    }
}
