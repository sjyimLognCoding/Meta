using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystickController;

    float movement_sensitivity = 0.1f;
    Vector3 initialRight, initialForward;

    private void Start()
    {
        Application.targetFrameRate = 30;
        joystickController.PointerDownEvent += updateInitialPositions;
    }
    void updateInitialPositions()
    {
        initialForward = transform.forward;
        initialRight = transform.right;
    }

    private void Update()
    {
        if (joystickController.JoystickEnabled && new Vector2(joystickController.Horizontal, joystickController.Vertical).magnitude > 0.03f)
        {
            JoystickControl();
        }
    }

    void JoystickControl()
    {
        //Vector3 dir = new Vector3(joystickController.Horizontal, 0, joystickController.Vertical);

        Vector3 newDir = (initialRight * joystickController.Horizontal + initialForward * joystickController.Vertical);

        transform.position += newDir.normalized * movement_sensitivity;

        //transform.eulerAngles = new Vector3(0, Mathf.Atan2(newDir.x, newDir.z) * Mathf.Rad2Deg, 0);

        Quaternion rot = Quaternion.LookRotation(newDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);

        //Vector2(Horizontal, vertical).magnitude 조건 나눠서 조이스틱 끝까지 땡겼냐/아니냐 구
    }
}
