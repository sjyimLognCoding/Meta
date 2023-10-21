using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectData : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
    }

    public void SaveData(ref GameData data)
    {
        data.position = transform.position;
        data.rotation = transform.rotation;
    }
}
