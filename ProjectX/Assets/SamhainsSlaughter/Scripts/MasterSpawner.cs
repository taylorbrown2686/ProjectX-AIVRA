using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    private MonsterSpawner[] monsterSpawners;
    private bool dontFire = false;

    void Start() {
      monsterSpawners = (MonsterSpawner[])FindObjectsOfType(typeof(MonsterSpawner));
    }

    void Update() {
      if (!dontFire) {
        StartCoroutine(StartSpawn());
      }
    }

    private IEnumerator StartSpawn() {
      dontFire = true;
      monsterSpawners[Random.Range(0, monsterSpawners.Length - 1)].Spawn();
      yield return new WaitForSeconds(2.5f);
      dontFire = false;
    }
}
