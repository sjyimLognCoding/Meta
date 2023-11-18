using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControlTest : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;

    float move_sensitivity = 0.03f;

    Vector3 initialRight, initialForward;

    bool initialPositionUpdated;


    private void Start()
    {
        Application.targetFrameRate = 30;
        joystick.PointerDownEvent += UpdateInitialPosition;
        joystick.PointerUpEvent += UpdateFlag;
    }

    void UpdateInitialPosition()
    {
        initialForward = transform.forward;
        initialRight = transform.right;
        initialPositionUpdated = true;
    }

    void UpdateFlag()
    {
        initialPositionUpdated = false;
    }

    private void Update()
    {
        if (joystick.JoystickEnabled && new Vector2(joystick.Horizontal, joystick.Vertical).magnitude > 0.05f && initialPositionUpdated)
        {
            JoystickControl();
        }

    }

    void JoystickControl()
    {
        Vector3 dir = (initialRight * joystick.Horizontal + initialForward * joystick.Vertical);
        Debug.DrawRay(transform.position, dir, Color.red);

        Vector3 dirNorm = dir.normalized;
        transform.position += dir * move_sensitivity;

        Quaternion newRot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = newRot;
    }
}
