using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : ShootableObject
{

    private HealthController healthController;

    public override void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(5,10) * speedMultiplier);
    }

    public override void Update() {
      if (this.gameObject.transform.position.y > initialY + 0.25f && objectCanBeShot) {
        StartCoroutine(Vanish());
      }
    }

    public virtual void OnTriggerEnter(Collider col) {
      if (col.tag == "Bullet") {
        healthController = GameObject.Find(col.gameObject.GetComponent<Bullet>().shotBy).GetComponent<HealthController>();
        healthController.PowerUp();
        PowerupUI();
        StartCoroutine(Vanish());
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
