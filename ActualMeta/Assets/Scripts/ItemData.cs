using System.Text.RegularExpressions;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[System.Serializable]
public class ItemData : MonoBehaviour, IDataPersistence
{
    //To be attached to every furniture item in the scene
    public Vector3 position;
    public Quaternion rotation;
    public int likeCount;

    private void OnEnable()
    {
        string pattern = @"\(Clone\)";

        gameObject.name = Regex.Replace(gameObject.name, pattern, "");
    }

    public void SaveData(GameData data)
    {
        position = transform.position;
        rotation = transform.rotation;

        string newKey = gameObject.name + "__" + gameObject.GetComponent<PhotonView>().ViewID;
        if (!data.dataDictionary.ContainsKey(newKey))
        {
            data.dataDictionary.Add(newKey, this);
        }
        else
        {
            data.dataDictionary[newKey] = this;
        }

    }
}
