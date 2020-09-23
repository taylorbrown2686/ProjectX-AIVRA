using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public GameObject goodCube, evilCube;
    private bool canSpawn = true;

    void Update() {
      if (canSpawn) {
        StartCoroutine(SpawnCubes());
      }
    }

    private IEnumerator SpawnCubes() {
      canSpawn = false;
      for (int i = 0; i <= 5; i++) {
        Instantiate(goodCube, new Vector3(Random.Range(-3.75f, 3.75f), 0.5f, Random.Range(-3.75f, 3.75f)), Quaternion.identity, this.transform);
        Instantiate(evilCube, new Vector3(Random.Range(-3.75f, 3.75f), 0.5f, Random.Range(-3.75f, 3.75f)), Quaternion.identity, this.transform);
      }
      yield return new WaitForSeconds(10f);
      canSpawn = true;
    }
}
