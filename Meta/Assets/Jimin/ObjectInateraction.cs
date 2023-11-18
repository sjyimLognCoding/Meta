using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class ObjectInateraction : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] PhotonView view;

    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
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
                        Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(hit.transform.GetComponent<PhotonView>().OwnerActorNr);
                        view.RPC("InteractAvatar", RpcTarget.All, view.ViewID, hit.transform.GetComponent<PhotonView>().ViewID);
                    }
                }
            }
        }  
    }

    public void CheckObject(string key)
    {
        //is someone usign this object? false = no, true = yes
        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(key) || (bool)PhotonNetwork.CurrentRoom.CustomProperties[key]==false)
        {
            Interact(key);
        }
        
    }

    public void Interact(string key)
    {
        Hashtable hashtable = new Hashtable();
        hashtable[key] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

        //hashtable[key] = null;
        //PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    [PunRPC] 
    public void InteractAvatar(int senderviewID, int receiverviewID)
    {
        //use animator to play animation
        Animator senderAnimator = PhotonView.Find(senderviewID).transform.GetComponent<Animator>();
        Animator receiverAnimator = PhotonView.Find(receiverviewID).transform.GetComponent<Animator>();

        PhotonView senderView = PhotonView.Find(senderviewID);
        PhotonView receiverView = PhotonView.Find(receiverviewID);

        Vector3 senderToReceiverDirection = receiverView.transform.position - senderView.transform.position;
        Vector3 receiverToSenderDirection = senderView.transform.position - receiverView.transform.position;

        Quaternion SRLookRotation = Quaternion.LookRotation(senderToReceiverDirection);
        Quaternion RSLookRotation = Quaternion.LookRotation(receiverToSenderDirection);

        senderView.transform.rotation = SRLookRotation;
        receiverView.transform.rotation = RSLookRotation;

        senderView.GetComponent<Animator>().SetBool("wave", true);
        receiverView.GetComponent<Animator>().SetBool("wave", true);
    }
}
