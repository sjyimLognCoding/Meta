using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystickController;


    // PC : Window / Mac / Linux / etc - targetFramerate 60fps

    // mobile : 30 fps

    // 
    // Transform.position += some value;

    float movement_sensitivity = 0.1f;

    Vector3 initialRight, initialForward;

    private void Start()
    {
        Application.targetFrameRate = 30;
        joystickController.PointerDownEvent += UpdateInitialPositions;
    }

    void UpdateInitialPositions()
    {
        initialForward = transform.forward;
        initialRight = transform.right;
    }

    private void Update()
    {
        // 2가지 : joystick is active && (joystick handle position).magnitude > some value
        if (joystickController.JoystickEnabled &&
        new Vector2(joystickController.Horizontal, joystickController.Vertical).magnitude > 0.03f)
        {
            JoystickControl();
        }
    }

    void JoystickControl()
    {
        // current value of dir is relative to global coordinates.
        // Vector3 dir = new Vector3(joystickController.Horizontal, 0, joystickController.Vertical);

        Vector3 newDir = initialRight * joystickController.Horizontal + initialForward * joystickController.Vertical;


        // transform.position += dir.normalized * movement_sensitivity;
        transform.position += newDir.normalized * movement_sensitivity;

        // transform.eulerAngles = new Vector3(0, Mathf.Atan2(newDir.x, newDir.z) * Mathf.Rad2Deg, 0);

        Quaternion rot = Quaternion.LookRotation(newDir, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);

        // transform.rotation = rot;
    }
}

