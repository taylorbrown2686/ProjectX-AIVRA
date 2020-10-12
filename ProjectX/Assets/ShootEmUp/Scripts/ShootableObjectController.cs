using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjectController : MonoBehaviour
{

    [SerializeField] private Shooter[] shooters;
    private bool canShoot = true;
    private float delay = 0.25f; //This delays how often the objects are shot up

    //mahnoor
    public void StartGame()
    {
        StartCoroutine(Shoot());
    }

    //end mahnoor

    private IEnumerator Shoot() {
        while (true) {

            Shooter shooter = shooters[Random.Range(0, shooters.Length)];

            yield return new WaitForSeconds(delay);
            
            if (!shooter.SpawnedShootable)
            {
                shooter.PrepareFire();
            }
        } 
    }

}
