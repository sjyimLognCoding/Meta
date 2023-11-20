using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Photon.Pun.PhotonView))]
public class ItemData : MonoBehaviour, IDataPersistence
{
    //To be attached to every furniture item in the scene

    public string type;
    public Vector3 position;
    public Quaternion rotation;
    // public Note note;
    public int price;
    public string note;

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        int i = 0;
        while (true)
        {
            if (data.dataDictionary.ContainsKey(type)) i++;
            else break;
        }
        string newKey = type + "_" + i;
        data.dataDictionary.Add(newKey, this);
    }
}
