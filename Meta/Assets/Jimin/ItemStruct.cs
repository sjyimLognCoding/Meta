using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct ItemStruct
{
    public ItemType Type;

    public string ItemTag;
}

public enum ItemType
{
    Table,
    Chair,
    Carpet
}
