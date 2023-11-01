using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class touch : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {

            print("touch");
            Touch touch = Input.touches[0];
            if (IsPointerOverUI(touch))
            {
                print("over ui");
            }
        }
    }

    private bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = new Vector2(touch.position.x, touch.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        if (!EventSystem.current) return false;
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

}
