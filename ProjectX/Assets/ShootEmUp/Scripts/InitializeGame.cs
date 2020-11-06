using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script initializes the game. Each game will have one of these that handles turning on scripts and UI when the game starts
public class InitializeGame : MonoBehaviour
{

    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject multiplayerUI;
    [SerializeField] private string gameManager;
    [SerializeField] private GameObject defaultPlayer;
    private MonoBehaviour superGameManager;

    void Awake() {
        GameUI.SetActive(false);
      //switch (gameManager) {
      //  case "GameManager":
      //    superGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
      //  break;

      //  case "RoundController":
      //    superGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RoundController>();
      //  break;
     // }
     // superGameManager.enabled = false;
      //newPlayer.name = ""; NAME OF PLAYER FROM ROOM GOES HERE
    }

    public void InitializeGameScripts() {
   //   GameObject newPlayer = Instantiate(defaultPlayer, Vector3.zero, Quaternion.identity, Camera.main.transform);
     // superGameManager.enabled = true;
    }

    public void InitializeGameUI() {
        multiplayerUI.SetActive(true);
    }

}
