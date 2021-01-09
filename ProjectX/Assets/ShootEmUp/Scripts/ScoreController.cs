using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int currentScore, compositeScore;
    public int CurrentScore {get => currentScore; set => currentScore = value;}
    public int CompositeScore {get => compositeScore;}
    public bool doublePoints = false; //For the 2x power-up

    void Update() {
        RewardsController.Instance.CompareScore(compositeScore);
    }

    public void AddScore(int scoreToAdd) {
      if (!doublePoints) {
        currentScore += scoreToAdd;
      } else {
        currentScore += (scoreToAdd * 2);
      }
    }

    public void AddScoreToComposite(int scoreToAdd) {
      compositeScore += scoreToAdd;
        Debug.Log(currentScore + ", " + compositeScore);
    }

    public IEnumerator PowerUp() {
      doublePoints = true;
      yield return new WaitForSeconds(10f);
      doublePoints = false;
    }

}
