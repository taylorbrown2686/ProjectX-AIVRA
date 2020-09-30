using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{

    private Rigidbody rb;
    private bool hasTicked = false; //One-sided colliders are inefficient, so this compensates for that
    [SerializeField] private float speedMultiplier;

    void Awake() {
      rb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Fire() {
      rb.AddForce(transform.up * Random.Range(15,20) * speedMultiplier);
    }

    void OnTriggerEnter(Collider col) {
      if (!hasTicked) {
        hasTicked = true;
      } else {
        Destroy(this.gameObject); //Destroy the object on it's second collision
      }
    }

}
