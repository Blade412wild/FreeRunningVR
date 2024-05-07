using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lookingtest : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 5f;

    // Axis of rotation
    public Vector3 rotationAxis = Vector3.up;

    private void Start()
    {
        // Lock the cursor in the middle of the screen
        //Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        //Cursor.visible = true;
    }
    void Update()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;

        // Calculate the screen midpoint
        Vector3 screenMidPoint = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Calculate the offset from the midpoint
        Vector3 offset = mousePos - screenMidPoint;

        // Calculate the angle to rotate around the specified axis
        float angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;

        // Create a rotation quaternion based on the angle
        Quaternion targetRotation = Quaternion.AngleAxis(angle, rotationAxis);

        // Rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

   
}
