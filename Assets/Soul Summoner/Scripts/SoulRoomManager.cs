using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulRoomManager : MonoBehaviourPunCallbacks
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

        //    print(CalculateDistance(44.8146590f, 44.8147742f, -91.503163685f, -91.50146757f));
    }

    // Update is called once per frame
    void Update()
    {
        /*   WWWForm form = new WWWForm();
           form.AddField("email", userEmail);
           WWW www = new WWW("http://65.52.195.169/AIVRA-PHP/pullFriends.php", form);
           yield return www;
           form.data
           */
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
                tx.color = Color.gray;
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
            if (room.PlayerCount > 0)
            {
                foreach (Transform child in roomsUI.transform)
                {
                    Text tx = child.gameObject.GetComponent<Text>();
                    if (tx.text == room.Name)
                        pass = true;
                }

                if (pass == true)
                {
                    pass = false;
                    break;
                }
                //   print(room.CustomProperties["longitude"]);
                //   print(room.CustomProperties["latitude"]);
                //   print(room.CustomProperties["location"]);

                if (CalculateDistance(Input.location.lastData.latitude, (float)room.CustomProperties["latitude"], Input.location.lastData.longitude, (float)room.CustomProperties["longitude"]) > 50)
                    continue;

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
        print(isVisible.isOn);
        ro.IsVisible = isVisible.isOn;
        ro.MaxPlayers = (byte)Convert.ToInt32(maxPlayers.text);

        print(roomName.text + " " + maxPlayers.text);

        PhotonNetwork.CreateRoom(roomName.text, ro);
        //  gameObject.SetActive(false);
    }

    public void JoinRoom()
    {
        print("attempt to join " + selectedRoom);
        PhotonNetwork.JoinRoom(selectedRoom);
        //   if (selectedRoom != null)
        //      gameObject.SetActive(false);
    }



    private float CalculateDistance(float lat_1, float lat_2, float long_1, float long_2)
    {
        int R = 6371;
        var lat_rad_1 = Mathf.Deg2Rad * lat_1;
        var lat_rad_2 = Mathf.Deg2Rad * lat_2;
        var d_lat_rad = Mathf.Deg2Rad * (lat_2 - lat_1);
        var d_long_rad = Mathf.Deg2Rad * (long_2 - long_1);
        var a = Mathf.Pow(Mathf.Sin(d_lat_rad / 2), 2) + (Mathf.Pow(Mathf.Sin(d_long_rad / 2), 2) * Mathf.Cos(lat_rad_1) * Mathf.Cos(lat_rad_2));
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var total_dist = R * c * 1000; // convert to meters
        return total_dist;
    }
}
