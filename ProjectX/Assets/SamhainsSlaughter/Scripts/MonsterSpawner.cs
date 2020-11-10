using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject portal;
    public GameObject monsterToSpawn;

    public void Spawn() {
      StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine() {
      GameObject newPortal = Instantiate(portal, this.transform.position, Quaternion.identity, this.transform);
      while (newPortal.transform.localScale.x < ScaleFactor.Instance.scaleFactor * 20) {
        newPortal.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      newPortal.GetComponentInChildren<ParticleSystem>().Play();
      yield return new WaitForSeconds(2f);
      GameObject newEnemy = Instantiate(monsterToSpawn, newPortal.transform.position, Quaternion.identity, this.transform);
      newEnemy.transform.localScale = new Vector3(
        ScaleFactor.Instance.scaleFactor / 5, ScaleFactor.Instance.scaleFactor / 5, ScaleFactor.Instance.scaleFactor / 5);
      while (newPortal.transform.localScale.x > 0) {
        newPortal.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        yield return new WaitForSeconds(0.01f);
      }
      Destroy(newPortal);
    }
}
