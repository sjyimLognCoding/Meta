using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    // Singleton pattern : one of popular design patterns
    // enforces a single instance of a class

    public static DataManager instance { get; private set; }
    // you are making sure only this script can make changes to this instance

    public GameData gameData;
    public List<IData> dataObjectList;

    private JsonReadWrite jsonLoader;

    private void Awake()
    {
        // this : refers to the current object of this class
        if (instance != null && instance != this)
        {
            Debug.Log("There is another instance");
            Destroy(this);
        }
        instance = this;
    }


    public void NewGame()
    {
        // create a new instance/object of GameData class
        gameData = new GameData();
    }

    public void SaveGame()
    {
        // todo : send signal to other scripts to update GameData
        foreach (IData data in dataObjectList)
        {
            data.SaveData(ref gameData);
        }


        // todo : push the GameData to file handler to save to json file
        jsonLoader.Save(gameData);
    }

    public void LoadGame()
    {
        // todo : load saved data and distribute them to any script that requires the data
        gameData = jsonLoader.Load();

        // if no data can be loaded ==> there is no save file
        // create a new game data
        if (gameData == null)
        {
            NewGame();
        }

        foreach (IData data in dataObjectList)
        {
            data.LoadData(gameData);
        }
    }


    private List<IData> FindAllDataObjects()
    {
        IEnumerable<IData> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IData>();

        // IEnumerable is an interface that has a method which returns an IEnumerator interface

        // This is a read-only access to a collection of data

        return new List<IData>(dataObjects);
    }


    private void Start()
    {
        jsonLoader = new JsonReadWrite(Application.persistentDataPath, "saved_data");
        dataObjectList = FindAllDataObjects();

        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveGame();
        }
    }
}
