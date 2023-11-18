using System.IO;
using Photon.Pun;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    // public GameData Load()
    // {
    //     string fullPath = Path.Combine(dataDirPath, dataFileName);

    //     GameData loadedData = null;

    //     if (File.Exists(fullPath))
    //     {
    //         try
    //         {
    //             string dataToLoad = "";
    //             using (FileStream stream = new FileStream(fullPath, FileMode.Open))
    //             {
    //                 using (StreamReader reader = new StreamReader(stream))
    //                 {
    //                     dataToLoad = reader.ReadToEnd();
    //                 }
    //             }

    //             loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
    //         }

    //         catch (System.Exception e)
    //         {
    //             throw e;
    //         }
    //     }

    //     return loadedData;
    // }

    public void Load(string json)
    {
        // string fullPath = Path.Combine(dataDirPath, dataFileName);

        GameData loadedData = null;

        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

        loadedData = JsonUtility.FromJson<GameData>(json);

        foreach (string key in loadedData.dataDictionary.Keys)
        {
            int _index = key.IndexOf('_');
            string typeWithoutIndex = key.Substring(0, _index);
            if (!pool.ResourceCache.ContainsKey(typeWithoutIndex))
            {
                pool.ResourceCache.Add(key, PrefabReference.instance.Prefabs[key]);
            }
        }

        foreach (string key in loadedData.dataDictionary.Keys)
        {
            // PhotonNetwork.Instantiate()
            int _index = key.IndexOf('_');
            string typeWithoutIndex = key.Substring(0, _index);

            GameObject go = PhotonNetwork.Instantiate(typeWithoutIndex, loadedData.dataDictionary[key].position, loadedData.dataDictionary[key].rotation);
            ItemData goItemData = go.GetComponent<ItemData>();
            if (goItemData == null) go.AddComponent<ItemData>();
            goItemData = loadedData.dataDictionary[key];

            //todo : finish this
        }
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }

        catch (System.Exception e)
        {
            throw e;
        }
    }
}
