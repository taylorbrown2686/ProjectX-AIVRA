using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealProviderFromScore : DealProvider
{
    [SerializeField] private int scoreToReach; //This will come from the DB, hardcode for now (unserialize when done)
    private int currentScore;
    private bool hasWon = false;

    void Update() {
      if (currentScore > scoreToReach && !hasWon) {
        hasWon = true;
        //StartCoroutine(OnWin());
      }
    }

    public void GiveScore(int score) {
      currentScore = score;
    }
}
