using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int health;
    private int maxHealth = 3;

    void Awake() {
      health = 3;
    }

    public void DecreaseHealth() {
      health -= 1;
    }

    void Update() {
      if (health == 0) {
        //show ui overlay
        //disable shooting
        //reset health after round (RoundController)
      }
    }

    public void ResetHealth() {
      health = maxHealth;
    }


}
