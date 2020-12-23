using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script initializes the game. Each game will have one of these that handles turning on scripts and UI when the game starts
public class InitializeGame : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private string gameManager;
    [SerializeField] private GameObject diceCup;
    private MonoBehaviour superGameManager;
    public bool multiplayer = false;

    void Awake() {
      canvas.SetActive(false);
      switch (gameManager) {
        case "GameManager":
          superGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        break;

        case "RoundController":
          superGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundController>();
        break;
        case "AvatarManager":
            superGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AvatarManager>();
        break;
        case "Soul Summoner":
                var fooGroup = Resources.FindObjectsOfTypeAll<SoulGameGamager>();
                superGameManager = fooGroup[0].GetComponent<SoulGameGamager>();
        break;
            default:
           superGameManager=null;
        break;

        }
        if (superGameManager != null)
            superGameManager.enabled = false;
      //newPlayer.name = ""; NAME OF PLAYER FROM ROOM GOES HERE
    }

    public void InitializeGameScripts() {
        if (multiplayer == false)
            Instantiate(diceCup, new Vector3(transform.position.x + 4, transform.position.y + 4, transform.position.z + 0), Quaternion.identity, Camera.main.transform);
        else
            InitializeNetworkGameScripts();
        if (superGameManager != null) 
            superGameManager.enabled = true;

    }

    public void InitializeUI() {
      canvas.SetActive(true);
    }
    public void InitializeNetworkGameScripts() {
        GameObject newPlayer;
        if (gameManager == "Soul Summoner")
        {
            superGameManager.enabled = true;
            newPlayer = PhotonNetwork.Instantiate("Soul Summoner\\Avatar", new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
            
        else
            newPlayer = PhotonNetwork.Instantiate(diceCup.name, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
     //   newPlayer.transform.SetParent(Camera.main.transform,true);
    }
}
