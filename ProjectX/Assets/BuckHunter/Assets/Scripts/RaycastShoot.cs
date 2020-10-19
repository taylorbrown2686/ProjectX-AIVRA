using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{

    public int gunDamage = 1;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                        // Amount of force which will be added to objects with a rigidbody shot by the player
                                                                         // Holds a reference to the gun end object, marking the muzzle location of the gun
    public float bulletSpeed = 10;
    private Camera fpsCam;                                                // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private AudioSource gunAudio;                                        // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing
    Ray ray;
    public GameObject bullet;

    void Start()
    {


        // Get and store a reference to our Camera by searching this GameObject and its parents
        fpsCam = GetComponentInParent<Camera>();
    }


    void Update()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            //Debug.Log("sdfsd");
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Create a vector at the center of our camera's viewport
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;



            // Check if our raycast has hit anything
            if (Physics.Raycast(ray, out hit, 1000))
            {

                Debug.Log(hit.collider);

                // If there was a health script attached

                // Check if the object we hit has a rigidbody attached
                if (hit.rigidbody != null)
                {
                    // Add force to the rigidbody we hit, in the direction from which it was hit
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    Debug.Log(hit);
                    hit.collider.gameObject.GetComponent<Sheep>().GetFast();

                }
            }
        }

    }

    void Fire()
    {
        Rigidbody bulletClone = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody>();
        bulletClone.velocity = transform.forward * bulletSpeed;
        // You can also acccess other components / scripts of the clone
        //rocketClone.GetComponent<MyRocketScript>().DoSomething();
    }

}