using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script handles sending the round settings to the controller, and starting the first round.
public class RoundSettings : MonoBehaviour
{
    public GameObject roundSettingsPanel;
    public Text roundText, roundTimerText;
    public Slider rounds, roundTimer;
    public RoundController roundController;

    void Start() {
      roundSettingsPanel = this.gameObject;
    }

    void Update() {
      roundText.text = "Rounds: " + rounds.value;
      roundTimerText.text = "Round Timer: " + roundTimer.value;
    }

    public void StartGame() { //Public onclick button handler
      roundController.RoundsRemaining = Convert.ToInt32(rounds.value);
      roundController.RoundLength = Convert.ToInt32(roundTimer.value);
      roundSettingsPanel.SetActive(false);
      roundController.GameReadyToStart = true;
    }
}
