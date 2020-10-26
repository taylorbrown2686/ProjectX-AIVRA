using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
                        
    private float scale = 1.0f;
    
    public float bulletSpeed = 150.0f;

    bool reloading = false;

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
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1") && reloading == false)
        {
            ammo--;
            Fire();
            StartCoroutine(Reload());
        }
            //Debug.Log("sdfsd");


    }

    void Fire()
    {
        source.Play();
        GameManager.Instance.MakeAllDeersRun();
        reloading = true;
        Rigidbody bulletClone = Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation).GetComponent<Rigidbody>();
        bulletClone.velocity = Camera.main.transform.forward * bulletSpeed * scale;
        // You can also acccess other components / scripts of the clone
        //rocketClone.GetComponent<MyRocketScript>().DoSomething();
    }

    public void SetScale(float scale)
    {
        this.scale = scale/2;
        transform.localScale *= (scale / 2);
    }

    IEnumerator Reload()
    {
      
        yield return new WaitForSeconds(0.5f);
        reloading = false;
        
    }

}