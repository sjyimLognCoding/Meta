using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;

    public static DataPersistenceManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            // Destory(this);
        }
        instance = this;
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }

    public void NewData()
    {

    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
