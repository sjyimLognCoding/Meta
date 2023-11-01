using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable; // use Hashtable from this library, not from default System.Collections

public class SpawnPlayer : MonoBehaviour
{
    public GameObject femaleDummy;
    public GameObject maleDummy;

    public TextMeshProUGUI RPCtext;

    public PhotonView view;

    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    Animator anim; //client side

    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(femaleDummy.name, position, Quaternion.identity);
        //Photon에서 어떤 오브젝트가 생성이 되었는지 기억
        // 꼭 network object가 아니어도 됌
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     view.RPC("RPCMethod", RpcTarget.All);

        //     int num = Random.Range(0, 10);
        //     int num2 = 2;
        //     int num3 = 3;
        //     view.RPC("SameNum", RpcTarget.All, num, num2, num3);
        // }

        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateHashTable();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckHashTable();
        }
    }
    #region rpc
    // [PunRPC] // once this attribute is added, the following method becomes "Remote Procedure Call" method
    // void RPCMethod()
    // {
    //     int num = Random.Range(0, 10);
    //     RPCtext.text = num.ToString();
    // }

    // [PunRPC]
    // void SameNum(int num, int num2, int num3)
    // {
    //     RPCtext.text = num.ToString();
    // }
    #endregion

    // CustomProperties - Hashtable type
    // Dictionary : key-value pair


    // Hashtable : non-generic type (Key-value) : 타입이 정해져있지 않음



    void UpdateHashTable()
    {
        Hashtable hash = new Hashtable();
        hash["first key"] = 1;
        hash[1] = "second value";
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

        // in most of collections of data, to access a value, we use [index / key]

        // obj interaction
        // 
    }

    void CheckHashTable()
    {
        // print(PhotonNetwork.CurrentRoom.CustomProperties.Keys.ToString());
        foreach (var key in PhotonNetwork.CurrentRoom.CustomProperties.Keys)
        {
            print(key);
            print(PhotonNetwork.CurrentRoom.CustomProperties[key]);
        }
    }

    // key-value
    // key : name of the object, e.g. sandbag
    // value : boolean (is it interactable?)

    void OBJinteraction()
    {
        // PhotonNetwork.CurrentRoom.CustomProperties[name of the object]
        // if empty/null -> interaction
        // if true -> interaction

        // if false -> return / notify the user that they cannot interact with this

    }



}
