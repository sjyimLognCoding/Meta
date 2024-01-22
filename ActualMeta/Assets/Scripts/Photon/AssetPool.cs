using Photon.Pun;
using UnityEngine;

public class AssetPool : MonoBehaviourPunCallbacks
{
    public static AssetPool instance { get; private set; }

    public GameObject[] RoomAssets;

    public GameObject[] AvatarAssets;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        foreach (GameObject go in RoomAssets)
        {
            if (!pool.ResourceCache.ContainsKey(go.name))
            {
                pool.ResourceCache.Add(go.name, go);
            }
        }

        foreach (GameObject go in AvatarAssets)
        {
            if (!pool.ResourceCache.ContainsKey(go.name))
            {
                pool.ResourceCache.Add(go.name, go);
            }
        }
    }




}
