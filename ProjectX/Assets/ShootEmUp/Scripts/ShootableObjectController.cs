﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjectController : MonoBehaviour
{

    [SerializeField] private Shooter[] shooters;
    private bool canShoot = true;
    private float delay; //This delays how often the objects are shot up
    [SerializeField] private RoundController roundController;

    void Update() {
      if (canShoot) {
        StartCoroutine(Shoot());
      }
    }

    private IEnumerator Shoot() {
      delay = (roundController.RoundsRemaining + 1) * 0.2f; //Scale factor for increased spawns each round
      canShoot = false;
      Shooter shooter = shooters[Random.Range(0, shooters.Length)];
      if (!shooter.SpawnedShootable) {
        shooter.PrepareFire();
      }
      yield return new WaitForSeconds(delay);
      canShoot = true;
    }

}
