using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectData : MonoBehaviour, IDataPersistence
{
    public Note note;

    public void LoadData(GameData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
        note = data.note;
    }

    public void SaveData(ref GameData data)
    {
        data.position = transform.position;
        data.rotation = transform.rotation;
        data.note = note;
    }
}
