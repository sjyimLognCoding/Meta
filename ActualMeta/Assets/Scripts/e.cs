using UnityEngine;

public class e : MonoBehaviour
{
    string Message = "Hi,asdf";
    private void Start()
    {
        string[] a = Message.Split(',');
        foreach (string s in a)
        {
            print(s);
        }
    }
}
