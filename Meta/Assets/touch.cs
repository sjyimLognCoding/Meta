using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount == 2)
        {

        }
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                print("began");
            }
            if (touch.phase == TouchPhase.Moved)
            {
                print("moved");
            }
            if (touch.phase == TouchPhase.Ended)
            {
                print("ended");
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                print("stationary");
            }
        }


        if (Input.GetMouseButton(0))
        {
            //button is held down
        }
        if (Input.GetMouseButtonDown(0))
        {
            //on press
        }
        if (Input.GetMouseButtonUp(0))
        {
            //on release
        }
    }
}
