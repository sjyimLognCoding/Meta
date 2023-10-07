using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnobject : MonoBehaviour
{
    public GameObject copyObject;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(copyObject);
            CountObject.count += 1;
        
        }
    }
}
