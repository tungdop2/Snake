using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreatandJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoomText;
    public TMP_InputField joinRoomText;

    public void Awake()
    {
        createRoomText.text = "A";
        joinRoomText.text = "A";
    }
    public void CreateRoom()
    {
        string roomName = createRoomText.text;
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom()
    {
        string roomName = joinRoomText.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiplayScene");
    }
}
