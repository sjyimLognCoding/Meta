using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public string objType;
    public Vector3 position;
    public Quaternion rotation;

    public GameData()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }
}
