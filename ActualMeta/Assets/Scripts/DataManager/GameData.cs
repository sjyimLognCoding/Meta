[System.Serializable]
public class GameData
{
    public SerialisableDictionary<string, ItemData> dataDictionary;

    public GameData()
    {
        dataDictionary = new SerialisableDictionary<string, ItemData>();
    }
}
