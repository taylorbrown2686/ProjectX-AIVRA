using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script initializes the game. Each game will have one of these that handles turning on scripts and UI when the game starts
public class InitializeGame : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private RoundSettings roundSettings;
    [SerializeField] private GameObject defaultPlayer;

    void Awake() {
      canvas.SetActive(false);
      roundSettings.enabled = false;
      GameObject newPlayer = Instantiate(defaultPlayer, Vector3.zero, Quaternion.identity, Camera.main.transform);
      //newPlayer.name = ""; NAME OF PLAYER FROM ROOM GOES HERE
    }

    public void InitializeGameScripts() {
      roundSettings.enabled = true;
    }

    public void InitializeUI() {
      canvas.SetActive(true);
    }
}
