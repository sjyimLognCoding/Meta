using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notion : MonoBehaviour
{
    [SerializeField] Animator animator;
    int state = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            state = state < 2 ? state + 1 : 0;
            animator.SetInteger("animation", state);
        }
    }
}
