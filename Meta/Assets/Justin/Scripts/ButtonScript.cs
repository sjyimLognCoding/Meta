using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    public void ButtonClick()
    {
        string clickObject = EventSystem.current.currentSelectedGameObject.name;

        if (clickObject == "Button_Square")
        {
            GameObject a = GameObject.FindGameObjectWithTag("Cube");
            GameObject b = Instantiate(a);
            b.transform.Translate(new Vector3(79, 268, -92));
        }


        if (clickObject == "Button_Circle")
        {
            GameObject a = GameObject.FindGameObjectWithTag("Cylinder");


        }
    }
}
