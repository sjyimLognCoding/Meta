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


    public override void OnJoinedRoom()
    {
        // 
        PhotonNetwork.LoadLevel("PhotonScene");
    }
}
