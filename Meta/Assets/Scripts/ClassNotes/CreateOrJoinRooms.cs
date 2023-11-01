using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateOrJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField createField;
    [SerializeField] TMP_InputField joinField;

    //create room
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createField.text);
    }

    //join room
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }

    // the above two methods will connect your photon profile to photon "room" not the unity scene


    public override void OnJoinedRoom()
    {
        // 
        PhotonNetwork.LoadLevel("PhotonScene");
        // network object : PhotonView component
    }
}
