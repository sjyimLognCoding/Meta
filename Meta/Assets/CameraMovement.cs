using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    CinemachinePOV pov;

    Vector3 lastPosition, current, delta;

    float multiplier = 0.1f;

    private void Start()
    {
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                lastPosition = touch.position;
            }

            current = touch.position;
            delta = current - lastPosition;

            pov.m_HorizontalAxis.Value += delta.x * multiplier;
            pov.m_VerticalAxis.Value -= delta.y * multiplier;

            lastPosition = touch.position;
        }
    }
}
