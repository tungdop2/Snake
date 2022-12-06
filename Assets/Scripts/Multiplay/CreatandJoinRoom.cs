using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreatandJoinRoom : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("A");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("A");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiplayScene");
    }
}
