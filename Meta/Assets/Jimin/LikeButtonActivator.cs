using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LikeButtonActivator : MonoBehaviour
{
    //public UnityEvent clickObject = new UnityEvent();
    public GameObject likeActivator;
    public GameObject Buttons;

    private void Start()
    {

        likeActivator = this.gameObject;
        Buttons.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Item"))
                {
                    Item hitItem = hit.transform.GetComponent<Item>();
                    hitItem.ShowLikeButton(Buttons);
                }
            }
            else
            {
                if (Buttons.activeInHierarchy)
                {
                    Buttons.SetActive(false);
                }
            }
        }
    }
}
