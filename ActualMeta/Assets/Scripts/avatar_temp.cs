using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class avatar_temp : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;

    [SerializeField] Animator animator;

    float move_sensitivity = 0.005f;
    Vector3 initialRight, initialForward;


    private void Start()
    {
        joystick.PointerDownEvent += UpdateInitialPosition;
        SetAnim("isIdle");
    }

    void UpdateInitialPosition()
    {
        initialRight = transform.right;
        initialForward = transform.forward;
    }

    string[] animParam = new string[]
    {
        "isWave",
        "isIdle",
        "isWalk",
        "isTPose"
    };

    void SetAnim(string anim)
    {
        foreach (string str in animParam)
        {
            if (str == anim)
            {
                animator.SetBool(str, true);
            }

            else
            {
                animator.SetBool(str, false);
            }
        }
    }

    private void Update()
    {

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (new Vector2(joystick.Horizontal, joystick.Vertical).magnitude > 0.05f)
            {
                Vector3 dir = (initialRight * joystick.Horizontal + initialForward * joystick.Vertical);
                Vector3 dirNorm = dir.normalized;

                Quaternion newRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = newRot;
                transform.position += dir * move_sensitivity;
                if (!animator.GetBool("isWalk"))
                {
                    SetAnim("isWalk");
                }
            }

            else
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                    {
                        if (hit.transform.CompareTag("Item"))
                        {
                            SetAnim("isWave");
                        }
                    }

                    else
                    {
                        animator.SetBool("isWave", false);
                    }

                    if (animator.GetBool("isWalk"))
                    {
                        SetAnim("isIdle");
                    }
                }
            }
        }
    }
}
