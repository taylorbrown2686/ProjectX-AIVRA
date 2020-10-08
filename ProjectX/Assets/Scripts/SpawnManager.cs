using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class SpawnManager : PunBehaviour 
{

    public GameObject[] playerPrefabs;
    public Transform[] spawnPositions;

    public GameObject Plane;
    private int playerNumber = 0;

    public enum RaiseEventCodes
    {
        PlayerSpawnEventCode = 0
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }

    #region Photon Callback Methods
    void OnEvent(byte eventCode, object content, int senderId)
    {
        if (eventCode == (byte)RaiseEventCodes.PlayerSpawnEventCode)
        {

            object[] data = (object[])content;
            Vector3 receivedPosition = (Vector3)data[0];
            Quaternion receivedRotation = (Quaternion)data[1];
            int receivedPlayerSelectionData = (int)data[3];

            GameObject player = Instantiate(playerPrefabs[receivedPlayerSelectionData], receivedPosition + Plane.transform.position, receivedRotation);
            PhotonView _photonView = player.GetComponent<PhotonView>();
            _photonView.viewID = (int)data[2];

        }
    }

    public void Spawn()
    {
        if (PhotonNetwork.connectedAndReady)
        {
            playerNumber = PhotonNetwork.playerList.Length - 1;
            SpawnPlayer();
        }
    }

    #endregion


    #region Private Methods
    private void SpawnPlayer()
    {
        // object playerSelectionNumber;
        //if (PhotonNetwork.player.CustomProperties.TryGetValue(.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
        //{
        //  Debug.Log("Player selection number is " + (int)playerSelectionNumber);
       
       // int randomSpawnPoint = Random.Range(0, spawnPositions.Length - 1);
        Vector3 instantiatePosition = spawnPositions[playerNumber].position;
        Quaternion spawnRotation = spawnPositions[playerNumber].rotation;
        GameObject playerGameobject = Instantiate(playerPrefabs[0],instantiatePosition, spawnRotation);

        PhotonView _photonView = playerGameobject.GetComponent<PhotonView>();
        int viewID = PhotonNetwork.AllocateViewID();
        _photonView.viewID = viewID;

        object[] data = new object[]
        {
            playerGameobject.transform.position - Plane.transform.position, playerGameobject.transform.rotation, _photonView.viewID, 0
        };


        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache

        };

        bool reliable = true;
            
        PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.PlayerSpawnEventCode, data, reliable, raiseEventOptions);

    }



    #endregion




}
