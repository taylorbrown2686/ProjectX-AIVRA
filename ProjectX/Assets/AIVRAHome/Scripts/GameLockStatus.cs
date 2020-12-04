using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLockStatus : MonoBehaviour
{
    public SortedList<string, bool> gamesAndStatuses = new SortedList<string, bool>();

    private static GameLockStatus instance = null;

    public static GameLockStatus Instance { get => instance; }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        gamesAndStatuses.Add("Ghosts In The Graveyard", false);
        gamesAndStatuses.Add("HuntAR", false);
        gamesAndStatuses.Add("AR Bar Dice", false);
        gamesAndStatuses.Add("Samhains Revenge", false);
    }

    public void LockGames()
    { //no games[] array, disable all and enable if they permanently unlocked the game
        for (int i = 0; i < gamesAndStatuses.Count; i++)
        {
            gamesAndStatuses[gamesAndStatuses.Keys[i]] = false;
        }
    }

    public void UnlockGames(string[] games)
    {
        for (int i = 0; i < gamesAndStatuses.Count; i++)
        {
            foreach (string str in games)
            {
                Debug.Log(str.Equals(gamesAndStatuses.Keys[i]));
                if (str.Equals(gamesAndStatuses.Keys[i]))
                {
                    gamesAndStatuses[gamesAndStatuses.Keys[i]] = true;
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
