using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class touch_camera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachinePOV pov;

    Vector3 lastPosition, current, delta;

    float multiplier = 0.05f;

    private void Start()
    {
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    void Update()
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
