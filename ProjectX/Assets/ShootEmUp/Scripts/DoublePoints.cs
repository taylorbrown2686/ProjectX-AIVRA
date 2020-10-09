﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePoints : ShootableObject
{

    private ScoreController playerHitScore;

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public override void Update() {
      if (this.gameObject.transform.position.y > initialY + 0.25f && objectCanBeShot) {
        StartCoroutine(Vanish());
      }
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        playerHitScore = GameObject.Find(col.gameObject.GetComponent<Bullet>().shotBy).GetComponent<ScoreController>();
        playerHitScore.StartCoroutine(playerHitScore.PowerUp());
        //StartCoroutine(PowerupUI());
        StartCoroutine(EnemyDeath());
      }
    }

    public void ActivateByBomb(string shotBy) { //Need this for who shot the object
      playerHitScore = GameObject.Find(shotBy).GetComponent<ScoreController>();
      playerHitScore.StartCoroutine(playerHitScore.PowerUp());
    }

    private IEnumerator PowerupUI() {
      powerupImages[1].enabled = true;
      yield return new WaitForSeconds(5f);
      powerupImages[1].enabled = false;
    }
}
