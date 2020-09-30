using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script initializes the game. Each game will have one of these that handles turning on scripts and UI when the game starts
public class InitializeGame : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private RoundSettings roundSettings;

    void Awake() {
      canvas.SetActive(false);
      roundSettings.enabled = false;
    }

    public void InitializeGameScripts() {
      roundSettings.enabled = true;
    }

    public void InitializeUI() {
      canvas.SetActive(true);
    }
}
