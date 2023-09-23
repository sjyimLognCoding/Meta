using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PhotonRPC : MonoBehaviour
{
    //RPC : Remote Procedure Call

    [SerializeField] NavMeshAgent agent;
    RaycastHit hit;

    [SerializeField] PhotonView view;

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.tag == "Ground")
                    {
                        // SetAgentDestination(hit.point);
                        view.RPC("SetAgentDestination", RpcTarget.All, hit.point);
                        // view.RPC("SetDestinationCoroutine", RpcTarget.All, hit.point);
                    }

                    else if (hit.transform.tag == "InteractableObject")
                    {
                        // TODO : block interaction
                        ObjectInteraction(hit.transform.name);
                    }


                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties);
        }
    }


    [PunRPC]
    private void SetAgentDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    [PunRPC]
    private IEnumerator SetDestinationCoroutine(Vector3 target)
    {
        agent.SetDestination(target);
        yield break;
    }








    // Photon Room/player custom properties
    // Dictionary/HashTable format

    ExitGames.Client.Photon.Hashtable roomProperty = new ExitGames.Client.Photon.Hashtable();

    private void ObjectInteraction(string name)
    {
        //TODO : check for custom property first:
        // if true, debug.log something

        if (PhotonNetwork.CurrentRoom.CustomProperties[name] != null &&
        (bool)PhotonNetwork.CurrentRoom.CustomProperties[name] == true)
        {
            Debug.LogError("Cube already in use!!!");
        }





        // if false, run below

        roomProperty[name] = true; // {Key-Value}
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperty);

        // PhotonNetwork.LocalPlayer.SetCustomProperties();




        //Tmr

        // Json : json 
    }
}
