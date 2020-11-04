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
    public string playerName; //Set when players enter game
    public bool useAmmo = true;
   
    private AudioSource source;
    PhotonView photonview;
    [SerializeField] private AudioClip gunshotSound, reloadSound;

    void Awake() {
      source = Camera.main.GetComponent<AudioSource>();
        photonview = GetComponent<PhotonView>();
       // playerName = this.gameObject.name; //Ambiguous, but assigning to a variable improves readability
        if (ammoText == null)
        {
            //mahnoor
            ammoText = GameController.instance.AmmoText;
            //end mahnoor
        }

    }

    void Update() {
      if (!useAmmo) {
        Debug.Log("inf ammo");
      }
        if (photonview.isMine)
        {
            if (Input.GetMouseButtonDown(0) && GameController.instance.gameState == GameState.Started)
            {
                photonview.RPC("Fire", PhotonTargets.All);
                //Fire();
            }
      }

      if (currentAmmo == 0 && !isReloading) {
        StartCoroutine(Reload());
      }
      if(ammoText != null)
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    [PunRPC]
    private void Fire() {


        if (photonview.isMine)
        {
            if (canShoot && currentAmmo != 0)
            {

                if (useAmmo)
                {
                    currentAmmo -= 1;
                }
                else
                {
                    currentAmmo = currentAmmo;
                }

                GameObject newBullet = PhotonNetwork.Instantiate(bullet.name, Camera.main.transform.position, Camera.main.transform.rotation, 0);
                //newBullet.transform.rotation = Quaternion.Euler(0, 90, 0);
                newBullet.GetComponent<Bullet>().shotBy = playerName;
                newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1000);
                StartCoroutine(DelayShooting());
            }
        }

        PlaySound(gunshotSound);
        
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
