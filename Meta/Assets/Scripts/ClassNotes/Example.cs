using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour, IData
{

    [SerializeField] SerializableDictionary<string, int> a;
    int aa = 0;
    string b = "0";

    private void Awake()
    {
        a = new SerializableDictionary<string, int>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            a.Add(b + aa.ToString(), aa);
            Debug.Log("adding value to serializable dict");
            aa++;
            Debug.Log(a);

            foreach (string i in a.Keys)
            {
                Debug.Log(i);
            }

            foreach (int i in a.Values)
            {
                Debug.Log(i);
            }

        }
    }


    public void SaveData(ref GameData data)
    {
        // update variable in GameData instance
        data.position = transform.position;
        data.rotation = transform.rotation;

        foreach (string k in a.Keys)
        {
            if (data.dataDictionary.ContainsKey(k))
            {
                data.dataDictionary.Remove(k);
            }

            // data.dataDictionary.Add(k, a[k]);
        }
    }

    public void LoadData(GameData data)
    {
        // load my position from GameData
        transform.position = data.position;
        transform.rotation = data.rotation;

        // this.a = data.dataDictionary;
    }

    // all the movement logic
}
