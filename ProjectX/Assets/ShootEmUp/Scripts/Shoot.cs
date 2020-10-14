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
    public string playerName = "Player 1"; //Set when players enter game
    public bool useAmmo = true;
    PhotonView photonview;
    private AudioSource source;
    private GameState gameState;
    [SerializeField] private AudioClip gunshotSound, reloadSound;

    void Awake() {
      source = Camera.main.GetComponent<AudioSource>();
        photonview = GetComponent<PhotonView>();
     // playerName = this.gameObject.name; //Ambiguous, but assigning to a variable improves readability
        //if (ammoText == null)
        //{
        //    ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        //}
    }

    void Update() {

        if (photonview.isMine)
        {
            if (Input.GetMouseButtonDown(0) && gameState == GameState.Started)
            {
               // Debug.Log("here");
                //s  Touch touch = Input.GetTouch(0);
                Fire();
            }
        }

      if (currentAmmo == 0 && !isReloading) {
        StartCoroutine(Reload());
      }
      if(ammoText != null)
      ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    void Fire() {
        if (canShoot ) //&& currentAmmo != 0
        {
            //if (useAmmo)
            //{
            //    currentAmmo -= 1;
            //}
            //else
            //{
            //    currentAmmo = currentAmmo;
            //}

            GameObject newBullet = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation);
            //newBullet.transform.rotation = Quaternion.Euler(0, 90, 0);
            newBullet.GetComponent<Bullet>().shotBy = playerName;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1000);
            PlaySound(gunshotSound);
            StartCoroutine(DelayShooting());
        }
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

    public IEnumerator PowerUp() {
      useAmmo = false;
      yield return new WaitForSeconds(10f);
      useAmmo = true;
    }
}
