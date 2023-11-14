using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WebSocketSharp;

public class RoomEditManager : MonoBehaviour
{
    #region UserBudget
    [SerializeField] private TextMeshProUGUI budget_text;
    [SerializeField] private TMP_InputField budget_input;
    public int user_budget_int
    {
        get => budget;
        private set
        {
            budget = value;
            budget_text.text = value.ToString();
        }
    }
    private int budget;
    #endregion

    #region ObjectInstantiation
    private Dictionary<string, GameObject> ObjectDictionary = new Dictionary<string, GameObject>();
    private string[] ObjectDictKeys = new string[] {
        "cube",
        "sphere"
    };
    [SerializeField] private GameObject[] ObjectDictValues;

    // option 1. another dictionary that holds objType-cost pair
    Dictionary<string, int> objCost = new Dictionary<string, int>();

    // option 2. array with multi dimensions
    // [[objType1, GameObject1, cost1],
    // [objType2, GameObject2, cost2] ]
    #endregion

    private void Start()
    {
        for (int i = 0; i < ObjectDictKeys.Length; i++)
        {
            ObjectDictionary.Add(ObjectDictKeys[i], ObjectDictValues[i]);
        }
    }

    public void CreateObject(string objectType)
    {
        GameObject go = Instantiate(ObjectDictionary[objectType], new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)), Quaternion.identity);
    }

    public void SetBudget(string user_input)
    {
        try
        {
            user_input = budget_input.text.IsNullOrEmpty() ? "0" : budget_input.text;
            user_budget_int = int.Parse(user_input);
        }

        catch (System.Exception e)
        {
            throw e;
        }
    }

}
