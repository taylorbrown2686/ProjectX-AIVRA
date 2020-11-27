using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateJoinRoomManager : MonoBehaviourPunCallbacks
{
    Button joinRoomButton;
    Button createRoomButton;
    DiceNetworkManager dnm;
    public string selectedRoom;
    public GameObject roomsUI;
    public Text roomText;
    public Text roomName;
    public Text maxPlayers;
    public Toggle isVisible;
    // Start is called before the first frame update
    void Start()
    {
        selectedRoom = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RoomSelected(string roomName)
    {
        selectedRoom = roomName;
        foreach (Transform child in roomsUI.transform)
        {
            Text tx = child.gameObject.GetComponent<Text>();
            if (tx.text == selectedRoom)
                tx.color = Color.green;
            else
                tx.color = Color.black;
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print(roomList.Count);
        /* foreach (Transform child in roomsUI.transform)
         {
             GameObject.Destroy(child.gameObject);
         }*/

        bool pass = false;

        foreach (RoomInfo room in roomList)
        {
            if(room.PlayerCount > 0) {
                foreach (Transform child in roomsUI.transform)
                {
                    Text tx = child.gameObject.GetComponent<Text>();
                    if (tx.text == room.Name)
                        pass = true;
                }
                
                if(pass == true)
                {
                    pass = false;
                    break;
                }


                Text rt = Instantiate(roomText, Vector3.zero, Quaternion.identity);
                rt.text = room.Name;

                rt.gameObject.GetComponent<Button>().onClick.AddListener(() => RoomSelected(room.Name));
                rt.gameObject.transform.SetParent(roomsUI.transform);

            }

            if (room.PlayerCount == 0)
            {
                foreach (Transform child in roomsUI.transform)
                {
                    Text tx = child.gameObject.GetComponent<Text>();
                    if (tx.text == room.Name)
                        Destroy(tx.gameObject);
                }
            }
        }
    }

    public void CreateRoom()
    {
        RoomOptions ro = new RoomOptions();

        ro.IsOpen = true;
        ro.IsVisible = isVisible.isOn;
        ro.MaxPlayers = (byte)Convert.ToInt32(maxPlayers);

        print(roomName.text + " " + (byte)Convert.ToInt32(maxPlayers));

        PhotonNetwork.CreateRoom(roomName.text, ro);
        gameObject.SetActive(false);
    }

    public void JoinRoom()
    {
        print(selectedRoom);
        PhotonNetwork.JoinRoom(selectedRoom);
        if (selectedRoom != null)
            gameObject.SetActive(false);
    }

}
