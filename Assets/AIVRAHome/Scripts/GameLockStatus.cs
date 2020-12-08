using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLockStatus : MonoBehaviour
{
    public Image grayBox;
    public Text gameTitle;
    public AIVRASays aivraSays;
    [SerializeField] private Dictionary<string, bool> gamesAndStatuses = new Dictionary<string, bool>();

    void Start() {
      gamesAndStatuses.Add("Ghosts in the Graveyard", false);
      gamesAndStatuses.Add("HuntAR", false);
      gamesAndStatuses.Add("Bar Dice", false);
      gamesAndStatuses.Add("AR Tetris", false);
      gamesAndStatuses.Add("AR Fishin'", false);
    }

    void Update() {
      if (gameTitle.text == "Ghosts in the Graveyard") {
        grayBox.enabled = false;
      } else {
        grayBox.enabled = !gamesAndStatuses[gameTitle.text];
      }
    }

    public void LockGames() { //no games[] array, disable all and enable if they permanently unlocked the game
      List<string> tempKeyList = new List<string>(gamesAndStatuses.Keys);
      foreach (string key in tempKeyList) {
        gamesAndStatuses[key] = false;
      }
    }

    public void UnlockGames(string[] games) {
      foreach (string game in games) {
        gamesAndStatuses[game] = true;
      }
    }

    public void PermanentlyUnlockGame() {
      //for database, takes account name and game unlocked and keeps it unlocked when they are in the app
    }

    public void BlockedPlayButtonOnclick() {
      aivraSays.StartCoroutine(aivraSays.Say("That game isn't available at this location. Purchase the game to play it anywhere, anytime!"));
    }
}
