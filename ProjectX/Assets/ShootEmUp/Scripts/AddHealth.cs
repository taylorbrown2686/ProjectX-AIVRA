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
        //StartCoroutine(PowerupUI());
        StartCoroutine(EnemyDeath());
      }
    }

    public void ActivateByBomb(string shotBy) { //Need this for who shot the object
      healthController = GameObject.Find(shotBy).GetComponent<HealthController>();
      healthController.PowerUp();
    }

    private IEnumerator PowerupUI() {
      powerupImages[1].enabled = true;
      yield return new WaitForSeconds(5f);
      powerupImages[1].enabled = false;
    }
}
