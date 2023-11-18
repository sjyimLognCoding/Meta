using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{

    public GameObject gameobject1;
    public GameObject gameobject2;
    public GameObject gameobject3;
    public GameObject gameobject4;

    public Boolean a = true;
    
    // Start is called before the first frame update
    public void OnClick()
    {
        if (a)
        {
            gameobject1.SetActive(false);
            gameobject2.SetActive(false);
            gameobject3.SetActive(false);
            gameobject4.SetActive(false);
        }
        else 
        {
            gameobject1.SetActive(true);
            gameobject2.SetActive(true);
            gameobject3.SetActive(true);
            gameobject4.SetActive(true); 
        }
    }
}
