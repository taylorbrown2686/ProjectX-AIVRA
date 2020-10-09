using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script handles sending the round settings to the controller, and starting the first round.
public class RoundSettings : MonoBehaviour
{
    public RoundController roundController;
    public GameObject startGame;

    public void StartGame() { //Public onclick button handler
      roundController.RoundsRemaining = 10;
      roundController.RoundLength = 30;
      roundController.GameReadyToStart = true;
      startGame.SetActive(false);
    }
}
