using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script initializes the game. Each game will have one of these that handles turning on scripts and UI when the game starts
public class InitializeGame : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private RoundController roundController;
    [SerializeField] private GameObject defaultPlayer;

    void Awake() {
      canvas.SetActive(false);
      roundController.enabled = false;
      //newPlayer.name = ""; NAME OF PLAYER FROM ROOM GOES HERE
    }

    public void InitializeGameScripts() {
      GameObject newPlayer = Instantiate(defaultPlayer, Vector3.zero, Quaternion.identity, Camera.main.transform);
      roundController.enabled = true;
    }

    public void InitializeUI() {
      canvas.SetActive(true);
    }
}
