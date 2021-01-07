using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCommunication : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MasterSendScaleMessage()
    {
        GameObject gameZone = GameObject.FindGameObjectWithTag("GameZone");
        if (PhotonNetwork.LocalPlayer.IsMasterClient == true)
            photonView.RPC("SetScale", RpcTarget.Others, gameZone.transform.localScale.x, gameZone.transform.localScale.z);
    }

    [PunRPC]
    void SetScale(float x, float z)
    {
        Debug.Log("fixed scale!!!!!!!!!!!");
        GameObject gameZone = GameObject.FindGameObjectWithTag("GameZone");
        FixedScale fs = gameZone.GetComponent<FixedScale>();
        fs.enabled = true;
        fs.SetScale(x, z);
    }
}
