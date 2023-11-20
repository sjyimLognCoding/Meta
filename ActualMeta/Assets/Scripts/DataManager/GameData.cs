using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public SerialisableDictionary<string, ItemData> dataDictionary;

    public GameData()
    {
        dataDictionary = new SerialisableDictionary<string, ItemData>();
    }

}
