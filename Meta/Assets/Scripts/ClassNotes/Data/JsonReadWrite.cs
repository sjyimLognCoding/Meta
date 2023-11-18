using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonReadWrite
{
    private string dataDirectoryPath = "";
    private string dataFileName = "";

    public JsonReadWrite(string dataDirectoryPath, string dataFileName)
    {
        this.dataDirectoryPath = dataDirectoryPath;
        this.dataFileName = dataFileName;
    }

    public void Save(GameData data)
    {
        // path to save file
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);

        try
        {
            // this is where we run our code

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true); // 2nd parameter : bool

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception)
        {
            // this will only run if there is an error
            throw;
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirectoryPath, dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        return loadedData;
    }
}