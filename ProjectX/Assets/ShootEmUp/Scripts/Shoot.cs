using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Text ammoText;
    private bool canShoot = true;
    private bool isReloading = false;
    private int maxAmmo = 12;
    public int MaxAmmo {get => maxAmmo;}
    private int currentAmmo = 12;
    public int CurrentAmmo {set => currentAmmo = value;} //Used for RoundController to reset ammo at each round

    private AudioSource source;
    [SerializeField] private AudioClip gunshotSound, reloadSound;

    void Awake() {
      source = Camera.main.GetComponent<AudioSource>();
    }

    void Update() {
      if (Input.touchCount == 1) {
        Touch touch = Input.GetTouch(0);
        if (canShoot && currentAmmo != 0) {
          currentAmmo -= 1;
          GameObject newBullet = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation);
          //newBullet.transform.rotation = Quaternion.Euler(0, 90, 0);
          newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1000);
          PlaySound(gunshotSound);
          StartCoroutine(DelayShooting());
        }
      }
      if (currentAmmo == 0 && !isReloading) {
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
      isReloading = true;
      PlaySound(reloadSound);
      yield return new WaitForSeconds(2.5f);
      currentAmmo = maxAmmo;
      isReloading = false;
    }

    private void PlaySound(AudioClip clip) {
      source.PlayOneShot(clip);
    }
}
