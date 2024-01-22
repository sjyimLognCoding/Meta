using System.IO;
using Photon.Pun;
using UnityEngine;

public class CloudJsonHandler
{
    public GameData Load(string json)
    {
        // string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        try
        {
            loadedData = JsonUtility.FromJson<GameData>(json);
        }
        catch (System.Exception e)
        {
            throw e;
        }

        return loadedData;

        // DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

        // loadedData = JsonUtility.FromJson<GameData>(json);

        // foreach (string key in loadedData.dataDictionary.Keys)
        // {
        //     int _index = key.IndexOf('_');
        //     string typeWithoutIndex = key.Substring(0, _index);
        //     if (!pool.ResourceCache.ContainsKey(typeWithoutIndex))
        //     {
        //         pool.ResourceCache.Add(key, PrefabReference.instance.Prefabs[key]);
        //     }
        // }

        // foreach (string key in loadedData.dataDictionary.Keys)
        // {
        //     // PhotonNetwork.Instantiate()
        //     int _index = key.IndexOf('_');
        //     string typeWithoutIndex = key.Substring(0, _index);

        //     GameObject go = PhotonNetwork.Instantiate(typeWithoutIndex, loadedData.dataDictionary[key].position, loadedData.dataDictionary[key].rotation);
        //     ItemData goItemData = go.GetComponent<ItemData>();
        //     if (goItemData == null) go.AddComponent<ItemData>();
        //     goItemData = loadedData.dataDictionary[key];

        //     //todo : finish this
        // }
    }

    public string Save(GameData data)
    {
        string dataToSave = JsonUtility.ToJson(data, true);
        return dataToSave;
    }
}
