using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReference : MonoBehaviour
{
    public List<string> Key;
    public List<GameObject> Value;
    public Dictionary<string, GameObject> Prefabs;

    public static PrefabReference instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;

        for (int i = 0; i < Key.Count; i++)
        {
            Prefabs.Add(Key[i], Value[i]);
        }
    }
}
