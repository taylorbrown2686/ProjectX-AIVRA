﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjectController : MonoBehaviour
{

    [SerializeField] private Shooter[] shooters;
    private bool canShoot = true;
    private float delay = 0.25f; //This delays how often the objects are shot up

    void Update() {
      if (canShoot) {
        StartCoroutine(Shoot());
      }
    }

    private IEnumerator Shoot() {
      canShoot = false;
      Shooter shooter = shooters[Random.Range(0, shooters.Length)];
      if (!shooter.SpawnedShootable) {
        shooter.PrepareFire();
      }
      yield return new WaitForSeconds(delay);
      canShoot = true;
    }

}
