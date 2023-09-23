using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PhotonView pv;



    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            float x_axis = Input.GetAxis("Horizontal");
            float z_axis = Input.GetAxis("Vertical");


            transform.position += new Vector3(x_axis * 0.01f, 0, z_axis * 0.01f);
        }
    }
}
