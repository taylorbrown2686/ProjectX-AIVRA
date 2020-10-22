using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : ShootableObject
{

    private HealthController healthController;
    private AudioSource audio;

    void Start() {
      audio = this.GetComponent<AudioSource>();
    }

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public override void Update() {
      if (this.gameObject.transform.position.y > initialY + (0.25f * scaleFactor) && objectCanBeShot) {
        Destroy(this.gameObject);
      }
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        healthController = GameObject.Find(col.gameObject.GetComponent<Bullet>().shotBy).GetComponent<HealthController>();
        healthController.PowerUp();
        PowerupUI();
        audio.Play();
        //StartCoroutine(Vanish());
        Destroy(this.gameObject);
      }
    }

    public void ActivateByBomb(string shotBy) { //Need this for who shot the object
      healthController = GameObject.Find(shotBy).GetComponent<HealthController>();
      healthController.PowerUp();
    }

    private void PowerupUI() {
      powerupUI.StartCoroutine(powerupUI.ChangeImage(3));
    }
}
