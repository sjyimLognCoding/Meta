using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountObject : MonoBehaviour
{
    public static int count = 0;
    public Text ScriptTxt;

    void Start()
    {
        ScriptTxt.text = "0";
    }

    void Update()
    {
        ScriptTxt.text = CountObject.count.ToString();
    }
}
