using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStuff : MonoBehaviour
{
    [SerializeField] GameObject[] TypeButtonGroup;



    public void ShowButtons()
    {
        foreach (GameObject go in TypeButtonGroup)
        {
            go.SetActive(true);
        }
    }
}
