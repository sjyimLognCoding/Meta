using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 position;
    public Quaternion rotation;

    public SerializableDictionary<string, int> ForTesting;


    // add whichever data you would like to save here

    // constructor
    public GameData()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;

        ForTesting = new SerializableDictionary<string, int>();

        // default value if there is no save file to load from
    }
}