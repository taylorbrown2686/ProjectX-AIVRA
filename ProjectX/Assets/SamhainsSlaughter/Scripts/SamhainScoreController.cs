using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamhainScoreController : MonoBehaviour
{
    public int score;
    private static SamhainScoreController _instance;

    public static SamhainScoreController Instance {get => _instance;}

    void Start() {
      if (_instance != null && _instance != this) {
        Destroy(_instance);
      } else {
        _instance = this;
      }
    }

    public void AddScore(int scoreToAdd) {
      score += scoreToAdd;
    }

}
