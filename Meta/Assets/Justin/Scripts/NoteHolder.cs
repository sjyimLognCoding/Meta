using UnityEngine;


[System.Serializable]
public struct Note
{
    public string ThisNote;
    public int Price;
    public string ItemType;
}


public class NoteHolder : MonoBehaviour
{
    public Note myNote;

    private void Start()
    {
        myNote.Price = ConstData.ItemPrice["Table"];
        myNote.ItemType = ConstData.ItemType[0];
    }
}
