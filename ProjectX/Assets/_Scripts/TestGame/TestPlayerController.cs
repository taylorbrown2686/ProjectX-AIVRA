using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerController : MonoBehaviour
{
    private int score = 0;
    public Text text;

    void OnTriggerEnter(Collider col) {
      if (col.gameObject.name.Contains("GoodCube")) {
        Destroy(col.gameObject);
        score += 1;
      } else if (col.gameObject.name.Contains("EvilCube")) {
        Destroy(col.gameObject);
        score -= 1;
      }
    }

    void Update() {
      text.text = "Score: " + score;
    }
}
