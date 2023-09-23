using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LikeButtonActivator : MonoBehaviour
{
    public UnityEvent clickObject = new UnityEvent();
    public GameObject likeActivator;
    public GameObject Buttons;

    private void Start()
    {

        likeActivator = this.gameObject;
        Buttons.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if (Buttons.activeSelf == false)
                {
                    clickObject.Invoke();
                    Buttons.SetActive(true);
                }
                else if (Buttons.activeSelf == true)
                {
                    Buttons.SetActive(false);
                }
            }
        }
    }
}
