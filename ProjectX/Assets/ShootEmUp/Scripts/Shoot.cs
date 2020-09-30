using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Text ammoText;
    private bool canShoot = true;
    private int maxAmmo = 12;
    private int currentAmmo = 12;

    void Update() {
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (canShoot && currentAmmo != 0) {
          currentAmmo -= 1;
          GameObject newBullet = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation);
          newBullet.transform.rotation = Quaternion.Euler(0, 90, 0);
          newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.right * 100);
          StartCoroutine(DelayShooting());
        }
      }
      if (currentAmmo == 0) {
        StartCoroutine(Reload());
      }
      ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    private IEnumerator DelayShooting() {
      canShoot = false;
      yield return new WaitForSeconds(0.5f);
      canShoot = true;
    }

    private IEnumerator Reload() {
      yield return new WaitForSeconds(3f);
      currentAmmo = maxAmmo;
    }
}
