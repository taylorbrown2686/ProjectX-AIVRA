using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePoints : ShootableObject
{

    private ScoreController playerHitScore;
    private AudioSource audio;

    void Start() {
      audio = this.GetComponent<AudioSource>();
    }

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public override void Update() {
      if (this.gameObject.transform.position.y > initialY + (0.25f * scaleFactor) && objectCanBeShot) {
        StartCoroutine(Vanish());
      }
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        playerHitScore = GameObject.Find(col.gameObject.GetComponent<Bullet>().shotBy).GetComponent<ScoreController>();
        playerHitScore.StartCoroutine(playerHitScore.PowerUp());
        PowerupUI();
        audio.Play();
        StartCoroutine(Vanish());
      }
    }

    public override IEnumerator Vanish() {
      isDying = true;
      foreach (Renderer renderer in this.gameObject.GetComponentsInChildren<Renderer>()) {
        StartCoroutine(FadeOut(renderer.material));
      }
      yield return new WaitForSeconds(5f);
      Destroy(this.gameObject);
    }

    private IEnumerator FadeOut(Material mat) {
      while (mat.color.a > 0) {
        mat.color -= new Color(0, 0, 0, .01f);
        yield return new WaitForSeconds(0.05f);
      }
    }

    public void ActivateByBomb(string shotBy) { //Need this for who shot the object
      playerHitScore = GameObject.Find(shotBy).GetComponent<ScoreController>();
      playerHitScore.StartCoroutine(playerHitScore.PowerUp());
    }

    private void PowerupUI() {
      powerupUI.StartCoroutine(powerupUI.ChangeImage(2));
    }
}
