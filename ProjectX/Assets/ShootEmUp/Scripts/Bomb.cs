using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ShootableObject
{

    private int scoreToAdd;
    private ParticleSystem explosion;
    private AudioSource audio;

    void Start() {
      explosion = this.gameObject.GetComponentInChildren<ParticleSystem>();
      audio = this.GetComponent<AudioSource>();
    }

    public override void Update() {
      if (this.gameObject.transform.position.y > initialY + (0.25f * scaleFactor) && objectCanBeShot) {
        StartCoroutine(Vanish());
      }
    }

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        StartCoroutine(Explode(col.gameObject.GetComponent<Bullet>().shotBy));
        PowerupUI();
        audio.Play();
      }
    }

    private IEnumerator Explode(string shotBy) {
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Shootable")) {
        if (Vector3.Distance(obj.transform.position, this.transform.position) < 0.1f) {
          if (Vector3.Distance(obj.transform.position, this.transform.position) == 0) { //The activated bomb is also checked
            continue; //Break out of the coroutine if we are referencing the bomb we hit
          }
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

    public override IEnumerator Vanish() {
      isDying = true;
      while (material.color.a > 0) {
        material.color -= new Color(0, 0, 0, .01f);
        yield return new WaitForSeconds(0.05f);
      }
      yield return new WaitForSeconds(5f);
      Destroy(this.gameObject);
    }

    public void ActivateByBomb(string shotBy) {
      StartCoroutine(Explode(shotBy));
    }

    private void PowerupUI() {
      powerupUI.StartCoroutine(powerupUI.ChangeImage(0));
    }
}
