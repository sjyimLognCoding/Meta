using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject femaleDummy;
    public GameObject maleDummy;


    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;


    private void Start()
    {
        Vector3 position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(femaleDummy.name, position, Quaternion.identity);
    }

}
