using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class MultiplayerManager : PunBehaviour
{
	private string _gameVersion = "1";

	public static string roomName;
	[SerializeField]
	private InputField RoomCode;

	private bool IsCreate=false;

    #region MonoBehaviour Callbacks
    private void Awake()
	{
		// #Critical
		// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
		PhotonNetwork.autoJoinLobby = false;

		// #Critical
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;

	}

	// Start is called before the first frame update
	void Start()
	{
		Connect();
	}

	// Update is called once per frame
	void Update()
	{

	}

    #endregion
    public void Connect()
	{
		// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
		//isConnecting = true;

		// hide the Play button for visual consistency
		//controlPanel.SetActive(false);

		// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
		if (!PhotonNetwork.connected)
		{ 
			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.ConnectUsingSettings(_gameVersion);

		}
	}

	string room;
	public void JoinRoom() {
		room = RoomCode.text;
		if (room != null)
			if (PhotonNetwork.connected)
			{
				PhotonNetwork.JoinRoom(room);
				UIManager.Instance.loadingPanel.SetActive(true);
			}

	}

	public void CreateRoom() {
		room = "room" + Random.Range(101, 99999);
		roomName = room;
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.IsOpen = true;
		roomOptions.MaxPlayers = 2;
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.CreateRoom(room);
			IsCreate = true;
			UIManager.Instance.loadingPanel.SetActive(true);
		}
	}

    #region Photon Callbacks
    public override void OnConnectedToPhoton()
    {
		Debug.Log("Connected");
        base.OnConnectedToPhoton();
    }

    public override void OnJoinedRoom()
    {
		roomName = room;
		UIManager.Instance.loadingPanel.SetActive(false);
		UIManager.Instance.roomPanel.SetActive(true);
		Debug.Log("Room Joined");
		PhotonNetwork.LoadLevel(1);
		UIManager.Instance.preJoinPanel.SetActive(false);
		UIManager.Instance.shareText.text = "Invite your friends with room code: " + roomName;
		if (IsCreate)
			Debug.Log("Invite your friends with room code: " + roomName);

        base.OnJoinedRoom();
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
		Debug.Log("Room Joining Failed");
		roomName = "";
		UIManager.Instance.loadingPanel.SetActive(false);
		UIManager.Instance.roomPanel.SetActive(false);
		base.OnPhotonJoinRoomFailed(codeAndMsg);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
		Debug.Log("New Player Arrived. Total Players: " + PhotonNetwork.room.PlayerCount);
        base.OnPhotonPlayerConnected(newPlayer);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
		Debug.Log("Player Left. Total Players: " + PhotonNetwork.room.PlayerCount);
		base.OnPhotonPlayerDisconnected(otherPlayer);
    }

	public override void OnDisconnectedFromPhoton()
    {
		//disconnect panel should pop up
        base.OnDisconnectedFromPhoton();
    }

    #endregion
}
