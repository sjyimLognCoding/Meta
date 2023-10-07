using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class wearItem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Item"))
                {
                    Item hitItem = hit.transform.GetComponent<Item>();
                    hitItem.AttachOnAvatar();
                }
                else if (hit.transform.CompareTag("AttachedItem"))
                {
                    //return to original displayed position
                }
            }
        }
    }
}
