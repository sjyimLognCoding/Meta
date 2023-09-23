using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMotion : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] Transform cameraTransform;

    public float speed = 0.02f;

    public Vector3 currentPosition;
    public Vector3 lastPosition;

    private void Update()
    {

        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = zDirection * camForward;
        Vector3 rightRelative = xDirection * camRight;

        Vector3 relativeMoveDir = forwardRelative + rightRelative;


        Vector3 moveDirection = new Vector3(relativeMoveDir.x, 0.0f, relativeMoveDir.z);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.position += moveDirection * speed * 2;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            transform.position += moveDirection * speed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }

        float facing = Camera.main.transform.eulerAngles.y;

        Vector3 myInputs = new Vector3(xDirection, 0, zDirection);

        Vector3 myTurnedInputs = Quaternion.Euler(0, facing, 0) * myInputs * 0.01f;

        transform.position += myTurnedInputs;

        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y,0);

        currentPosition = transform.position;

        if (currentPosition == lastPosition)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    private void LateUpdate()
    {
        lastPosition = currentPosition;

    }
}