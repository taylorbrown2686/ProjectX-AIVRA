using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLockStatus : MonoBehaviour
{
    public GameObject grayBox;
    public Text gameTitle;
    public AIVRASays aivraSays;
    [SerializeField] private SortedList<string, bool> gamesAndStatuses = new SortedList<string, bool>();

    void Start() {
      gamesAndStatuses.Add("Ghosts in the Graveyard", false);
      gamesAndStatuses.Add("HuntAR", false);
      gamesAndStatuses.Add("AR Bar Dice", false);
      gamesAndStatuses.Add("AR Tetris", false);
      gamesAndStatuses.Add("AR Fishin'", false);
    }

    void Update() {
      if (gameTitle.text == "Ghosts in the Graveyard") {
        grayBox.SetActive(false);
      } else {
        grayBox.SetActive(!gamesAndStatuses[gameTitle.text]);
      }
    }

    public void LockGames() { //no games[] array, disable all and enable if they permanently unlocked the game
      for (int i = 0; i < gamesAndStatuses.Count; i++) {
            gamesAndStatuses[gamesAndStatuses.Keys[i]] = false;
      }
    }

    public void UnlockGames(string[] games) {
      for (int i = 0; i < gamesAndStatuses.Count; i++) {
        foreach (string str in games) {
            if (str == gamesAndStatuses.Keys[i]) {
                gamesAndStatuses[gamesAndStatuses.Keys[i]] = true;
            }    
        }
      }
    }

    public void PermanentlyUnlockGame() {
      //for database, takes account name and game unlocked and keeps it unlocked when they are in the app
    }
}
