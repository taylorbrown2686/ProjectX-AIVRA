using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public int score;

    void Update() {
      scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int scoreToAdd) {
      score += scoreToAdd;
    }

}
