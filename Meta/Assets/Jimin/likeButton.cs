using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class likeButton : MonoBehaviour
{
    public Text likeCounterText;
    private int likeCount = 0;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                likeCount++;
                Debug.Log("Likes: " + likeCount.ToString());
            }
        }
    }
}
