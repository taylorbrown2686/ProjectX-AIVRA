using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamhainGun : MonoBehaviour
{
    //Gun Fields
    [SerializeField] private int maxAmmo;
    private int currentAmmo;
    [SerializeField] private float bulletDelay; //firerate
    [SerializeField] private float reloadSpeed;
    private bool canShoot = true;
    private bool isReloading = false;

    //Bullet Fields
    private int damage;
    private GameObject activeBullet;
    private string activeBulletType;
    [SerializeField] private GameObject[] bullets;

    void Start() {
      SwapBulletType("10mm");
    }

    void Update() {
      if (Input.touchCount == 1 && canShoot) {
        StartCoroutine(Shoot());
      }
    }

    private IEnumerator Shoot() {
      if (currentAmmo != 0) {
        canShoot = false;
        GameObject newBullet = Instantiate(activeBullet, Camera.main.transform.position, Camera.main.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 2);
        newBullet.GetComponent<Bullet>().damage = damage;
        currentAmmo -= 1;
        yield return new WaitForSeconds(bulletDelay);
        canShoot = true;
      } else {
        StartCoroutine(Reload());
      }
    }

    public void ReloadButton() {
      if (!isReloading) {
        StartCoroutine(Reload());
      }
    }

    public IEnumerator Reload() {
      canShoot = false;
      isReloading = true;
      //play sound
      yield return new WaitForSeconds(reloadSpeed);
      currentAmmo = maxAmmo;
      isReloading = false;
      canShoot = true;
    }

    private void SwapBulletType(string type) {
      switch (type) {
        case "10mm":
          activeBulletType = "10mm";
          activeBullet = bullets[0];
          damage = 15;
        break;
        case "Shell":
          activeBulletType = "Shell";
          activeBullet = bullets[1];
          damage = 30;
        break;
        case "Laser":
          activeBulletType = "Laser";
          activeBullet = bullets[2];
          damage = 20;
        break;
        case "Flamethrower":
          activeBulletType = "Flamethrower";
          activeBullet = bullets[3];
          damage = 5;
        break;
      }
    }
}
