using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARLocation;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy, instantiatedEnemy; //reference to enemy stage object
    public Transform arso; //reference to ARSessionOrigin transform (for parenting enemies)
    public PlaceAtLocation enemyLocation;

    public GameObject spawnScreen;
    public InputField lat, lng;

    public void OpenSpawnScreen() {
      spawnScreen.SetActive(true);
      lat.text = ""; //reset text fields
      lng.text = "";
    }

    public void Cancel() {
      spawnScreen.SetActive(false);
    }

    public void SpawnEnemyAtCoords() {
      instantiatedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity, arso); //instantiate an enemy as a child of the ARSO
      enemyLocation = instantiatedEnemy.GetComponent<PlaceAtLocation>();
      enemyLocation.Location = new Location(Convert.ToDouble(lat.text), Convert.ToDouble(lng.text), 0);
      spawnScreen.SetActive(false);
    }
}
