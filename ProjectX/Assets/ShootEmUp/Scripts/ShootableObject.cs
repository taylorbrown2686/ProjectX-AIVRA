using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObject : MonoBehaviour
{

    private Rigidbody rb;
    private bool hasTickedGround = false; //One-sided colliders are inefficient, so this compensates for that
    [SerializeField] private float speedMultiplier;
    [SerializeField] private ScoreController scoreController;

    void Awake() {
      rb = this.gameObject.GetComponent<Rigidbody>();
      //We do this because ScoreController is on a prefab, and can't be assigned manually
      scoreController = GameObject.Find("_GAMECONTROLLER").GetComponent<ScoreController>();
    }

    public void Fire() {
      rb.AddForce(transform.up * UnityEngine.Random.Range(50,60) * speedMultiplier);
    }

    void OnCollisionEnter(Collision col) {
      if (col.collider.tag == "Ground") {
        if (hasTickedGround) {
          Destroy(this.gameObject);
        } else {
          hasTickedGround = true;
        }
      } else if (col.collider.tag == "Bullet") {
        int scoreToAdd = Convert.ToInt32(this.gameObject.name.Substring(0, 1));
        scoreController.AddScore(scoreToAdd);
        Destroy(this.gameObject);
      }
    }

}
