using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToPhotonServer : MonoBehaviourPunCallbacks
{
    // This class inherits MonoBehaviourPunCallbacks which inherits MonoBehaviourPun which inherits MonoBehaviour

    // Callback functions : 특정 이벤트 시 신호를 주는 역할
    // callback 함수에 다음 실행해야할 로직을 연결하면 굳이 해당 이벤트가 완료되었는지 manual하게 체크 안해도되는 장점이 있음

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("connected to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
