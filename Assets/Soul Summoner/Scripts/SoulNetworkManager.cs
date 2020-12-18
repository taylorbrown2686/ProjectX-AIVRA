using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using UnityEngine.UI;

public class SoulNetworkManager : MonoBehaviourPunCallbacks
{


    public InputField nickName;
    public Button connectButton;

    public List<Player> players;
    public Image playerImage;
    public Canvas scoreBoard;
    public int counter;
    public SoulCommunication sc;
    public GameObject tutorial;
    public GameObject roomCanvas;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<Player>();
        //   photonView = GetComponent<PhotonView>();+
        

        Hashtable setPlayerProperties = new Hashtable();
        setPlayerProperties.Add("score", "null");
        
        //        PhotonNetwork.LocalPlayer.SetCustomProperties(setPlayerProperties);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        tutorial.SetActive(false);
        Destroy(nickName.gameObject, 1f);
        Destroy(connectButton.gameObject, 1f);
        roomCanvas.SetActive(true);
        PhotonNetwork.LocalPlayer.NickName = nickName.text;
        PhotonNetwork.ConnectUsingSettings();

    }


    public override void OnConnected()
    {
        Debug.Log("Connecting!!!");

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!!!");
        PhotonNetwork.JoinLobby();

    }


    public override void OnJoinedLobby()
    {
        Debug.Log("joined lobby!!!");
        // PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        

        roomCanvas.SetActive(false);
        tutorial.SetActive(true);
        Debug.Log("joined room!!!!!!!!");

        foreach (Player player in PhotonNetwork.PlayerList)
            players.Add(player);
        //  InitializeNetworkGameScripts
        SetScoreBoard();

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("no rooms");

        RoomOptions ro = new RoomOptions();



        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 8;

        PhotonNetwork.CreateRoom("room", ro);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print(message);
    }



    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " created a " + PhotonNetwork.CurrentRoom.Name);
        string[] lobbyprops = { "location", "longitude", "latitude" };

        Hashtable props = new Hashtable();
        props["longitude"] = Input.location.lastData.longitude;
        props["latitude"] = Input.location.lastData.latitude;
        props["location"] = "earth media";

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        PhotonNetwork.CurrentRoom.SetPropertiesListedInLobby(lobbyprops);

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        sc.MasterSendScaleMessage();
        players.Add(newPlayer);
        SetScoreBoard();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Player leavingPlayer = null;

        foreach (Player player in players)
            if (player.NickName == otherPlayer.NickName)
            {
                leavingPlayer = player;
                break;
            }
        players.Remove(leavingPlayer);
        SetScoreBoard();
    }

    void SetScoreBoard()
    {
        /*  GameObject playerImagetemp;

          foreach (Transform child in GameObject.FindGameObjectWithTag("ScoreBoard").transform)
          {
              GameObject.Destroy(child.gameObject);
          }

          for (int i = 0; i < players.Count; i++)
          {
              Debug.Log("created avatar");
              playerImagetemp = Instantiate(playerImage, new Vector3(-400 + i * 200, 960, playerImage.rectTransform.position.z), playerImage.transform.rotation).gameObject;

              DicePlayer dp = playerImagetemp.GetComponent<DicePlayer>();

              dp.nickname = dp.nicknameText.text = players[i].NickName;

              playerImagetemp.transform.SetParent(GameObject.FindGameObjectWithTag("ScoreBoard").transform);
              playerImagetemp.GetComponent<RectTransform>().localPosition = new Vector3(-400 + i * 200, 960, playerImage.rectTransform.position.z);
          }
          */
    }
}
