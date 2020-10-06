﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmo : ShootableObject
{

    private Shoot playerShoot;

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
      StartCoroutine(Vanish());
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        playerShoot = GameObject.Find(col.gameObject.GetComponent<Bullet>().shotBy).GetComponent<Shoot>();
        playerShoot.StartCoroutine(playerShoot.PowerUp());
        StartCoroutine(EnemyDeath());
      }
    }

    public void ActivateByBomb(string shotBy) {
      playerShoot = GameObject.Find(shotBy).GetComponent<Shoot>();
      playerShoot.StartCoroutine(playerShoot.PowerUp());
    }
}
