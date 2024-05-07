using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    public event Action OnJumpPressed;
    public Transform headTransform;

    [SerializeField] private float groeiFactor = 1.5f;
    private float beginvalue = 1;

    private Rigidbody rb;
    private Vector2 input = new Vector2(0,0);
    private Vector3 direction = new Vector3(0,0,0);
    public float speed;
    private bool MayMove = false;



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
        //Walk();
        if (Input.GetKeyDown(KeyCode.W))
        {
            MayMove = true;
        }
        
        if(Input.GetKeyUp(KeyCode.W))
        {
            MayMove = false;
        }

    }

    private void FixedUpdate()
    {
        WalkRigidbody();
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
        // n = g*B^t
        speed = groeiFactor * Mathf.Pow(1, Time.deltaTime); 

        direction = new Vector3(input.x, 0, input.y);
       
        
        transform.position += direction * Time.deltaTime;
    }

    private void WalkRigidbody()
    {
        if (!MayMove) return;
        //speed = 5;
        direction = headTransform.forward;
        //direction = headTransform.forward + headTransform.right;
        Debug.Log(direction);

        rb.AddForce(direction.normalized * speed * 10f, ForceMode.Force) ;
    }

}
