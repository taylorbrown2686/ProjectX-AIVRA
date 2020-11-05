using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
                        
    private float scale = 1.0f;
    
    public float bulletSpeed = 150.0f;

    bool reloading = true;

    private int ammo = 10;

    private AudioSource source;


    public GameObject bullet;
 //   Text text;
    GameObject ui;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("text");
        source = GetComponent<AudioSource>();
        //       text = ui.GetComponent<Text>();

    }


    void Update()
    {
    
        if (Input.GetButtonDown("Fire1") && reloading == false)
        {
            reloading = true;
            ammo--;
            Fire();
            StartCoroutine(Reload(0.5f));
        }
    
    }

    void Fire()
    {
        source.Play();
        GameManager.Instance.MakeAllDeersRun();
        Rigidbody bulletClone = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
        bulletClone.velocity = Camera.main.transform.forward * bulletSpeed * scale *2;
        
    }

    public void SetScale(float scale)
    {
        this.scale = scale/2;
        transform.localScale *= (scale / 2);
        bullet.transform.localScale *= scale/2;
    }

    public void setAmmo(int amount)
    {
        ammo = amount;
    }

    IEnumerator Reload(float delay)
    {


        yield return new WaitForSeconds(delay);
        {
            reloading = false;
        }
    }

    public void CanShoot(bool active)
    {
        reloading = !active;
    }

}