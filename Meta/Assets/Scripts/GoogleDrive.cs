using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleDrive : MonoBehaviour
{
    public static GoogleDrive instance { get; private set; }

    private const string jsonURL = "";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }



    IEnumerator GetDataFromDrive(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.LogError("Something went wrong");
        }
        else
        {
            GameData data = JsonUtility.FromJson<GameData>(request.downloadHandler.text);
        }
    }
}
