using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private CloudJsonHandler dataHandler;

    public static DataPersistenceManager instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void SaveData()
    {
        foreach (IDataPersistence dataObject in dataPersistenceObjects)
        {
            dataObject.SaveData(gameData);
        }

        dataHandler.Save(gameData);
    }

    public void LoadData()
    {
        // this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewData();
        }

        foreach (IDataPersistence dataObject in dataPersistenceObjects)
        {
            // dataObject.LoadData(gameData);
        }
    }

    public void NewData()
    {
        this.gameData = new GameData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadData();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
