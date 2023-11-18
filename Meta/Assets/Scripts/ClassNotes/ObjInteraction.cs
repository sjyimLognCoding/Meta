using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class ObjInteraction : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] PhotonView view;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "Item")
                    {
                        CheckObject(hit.transform.name);
                    }

                    else if (hit.transform.tag == "Avatar" && hit.transform != transform)
                    {
                        view.RPC("InteractAvatar", RpcTarget.All, view.ViewID, hit.transform.GetComponent<PhotonView>().ViewID);
                    }
                }
            }
        }
    }

    public void CheckObject(string key)
    {
        // is someone using this object? : false = 아무도 없음, true = 누군가 쓰는 중
        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(key) || (bool)PhotonNetwork.CurrentRoom.CustomProperties[key] == false)
        {
            InteractObj(key);
        }
    }

    public void InteractObj(string key)
    {
        Debug.Log("calling interact()");

        // play animation/vfx/sound/etc

        Hashtable hashtable = new Hashtable();
        hashtable[key] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

        // only delete key-value pair of custom properties when the value is null

        // hashtable[key] = null;
        // PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }



    // Avatar Interaction (RPC)
    // my avatar : turn to target avatar & play animation

    // target avatar : turn to my avatar & play animation
    [PunRPC]
    public void InteractAvatar(int senderViewID, int receiverViewID)
    {
        // use animator to play animation

        // Animator senderAnimator = PhotonView.Find(senderViewID).transform.GetComponent<Animator>();
        // Animator receiverAnimator = PhotonView.Find(senderViewID).transform.GetComponent<Animator>();

        PhotonView senderView = PhotonView.Find(senderViewID);
        PhotonView receiverView = PhotonView.Find(senderViewID);

        Vector3 senderToReceiverDirection = receiverView.transform.position - senderView.transform.position;
        Vector3 receiverToSenderDirection = senderView.transform.position - receiverView.transform.position;

        Quaternion SRLookRotation = Quaternion.LookRotation(senderToReceiverDirection);
        Quaternion RSLookRotation = Quaternion.LookRotation(receiverToSenderDirection);

        senderView.transform.rotation = SRLookRotation;
        receiverView.transform.rotation = RSLookRotation;

        senderView.GetComponent<Animator>().SetBool("wave", true);
        receiverView.GetComponent<Animator>().SetBool("wave", true);
    }

    // 3 clients: A B C
    // A waves to B & vice versa

}
