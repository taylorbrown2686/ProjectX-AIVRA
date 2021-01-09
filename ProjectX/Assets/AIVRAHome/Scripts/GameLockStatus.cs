using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLockStatus : MonoBehaviour
{
    public Dictionary<string, bool> gamesAndStatuses = new Dictionary<string, bool>();

    private static GameLockStatus instance = null;

    public static GameLockStatus Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        gamesAndStatuses.Add("Ghosts In The Graveyard", true);
        gamesAndStatuses.Add("HuntAR", true);
        gamesAndStatuses.Add("AR Bar Dice", true);
        gamesAndStatuses.Add("Samhains Revenge", true);
    }

    public void LockGames()
    { //no games[] array, disable all and enable if they permanently unlocked the game
        foreach (KeyValuePair<string, bool> pair in gamesAndStatuses) {
            if (pair.Key == "Ghosts In The Graveyard")
            {
                gamesAndStatuses[pair.Key] = true;
            }
            else
            {
                gamesAndStatuses[pair.Key] = true; //CHANGE THIS TO FALSE WHEN GAMES NEED TO BE LOCKED!!!
            }
        }
    }

    public void UnlockGames(string[] games)
    {
        foreach (KeyValuePair<string, bool> pair in gamesAndStatuses)
        {
            foreach (string str in games)
            {
                if (str.Equals(pair.Key))
                {
                    gamesAndStatuses[pair.Key] = true;
                }
            }
        }
    }

    public void PermanentlyUnlockGame()
    {
        //for database, takes account name and game unlocked and keeps it unlocked when they are in the app
    }

    public bool CheckIfGameUnlocked(string game)
    {
        foreach (KeyValuePair<string, bool> pair in gamesAndStatuses)
        {
            if (pair.Key == game)
            {
                return pair.Value;
            }
        }
        return false;
    }
}
