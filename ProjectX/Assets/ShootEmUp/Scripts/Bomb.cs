using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ShootableObject
{

    private int scoreToAdd;
    private ParticleSystem explosion;

    void Start() {
      explosion = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
      StartCoroutine(Vanish());
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        StartCoroutine(Explode(col.gameObject.GetComponent<Bullet>().shotBy));
      }
    }

    private IEnumerator Explode(string shotBy) {
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Shootable")) {
        if (Vector3.Distance(obj.transform.position, this.transform.position) < 3) {
          if (obj.name.Contains("2X")) {
            obj.GetComponent<DoublePoints>().ActivateByBomb(shotBy);
          } else if (obj.name.Contains("Bomb")) {
            obj.GetComponent<Bomb>().ActivateByBomb(shotBy);
          } else if (obj.name.Contains("MaxAmmo")) {
            obj.GetComponent<MaxAmmo>().ActivateByBomb(shotBy);
          } else {
            scoreToAdd += Convert.ToInt32(obj.name.Substring(0, 1));
          }
          Destroy(obj);
        }
      }
      explosion.Play();
      yield return new WaitForSeconds(1f);
      explosion.Stop();
      Destroy(this.gameObject);
    }

    public void ActivateByBomb(string shotBy) {
      StartCoroutine(Explode(shotBy));
    }
}
